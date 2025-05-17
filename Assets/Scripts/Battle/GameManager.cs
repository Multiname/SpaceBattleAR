using UnityEngine;
using UnityEngine.SceneManagement;

namespace System.Runtime.CompilerServices {
        internal static class IsExternalInit {}
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UiManager uiManager;
    [SerializeField]
    private NetworkTransmitter networkTransmitter;
    [SerializeField]
    private SpaceshipsManager spaceshipsManager;
    [SerializeField]
    private UnlockedCards unlockedCards;

    [SerializeField]
    private int winReward = 10;
    
    public int CurrentPlayerIndex {
        get => networkTransmitter.currentPlayerIndex;
        set => networkTransmitter.SetCurrentPlayerIndexServerRpc(value);
    }

    // DEBUG
    private void Start() {
        networkTransmitter.SetReadyServerRpc(new());
    }

    public void CreateBattlefield(GameObject origin) {
        spaceshipsManager.CreateBattlefield(origin);
        networkTransmitter.SetReadyServerRpc(new());
    }

    public void StartGame() {
        if (networkTransmitter.GetPlayerId() == CurrentPlayerIndex) {
            uiManager.PullCard();
            uiManager.SetNextTurnButtonActive(true);
        }
    }

    public bool TryToSpawnSpaceship(Spaceship spaceship) {
        var column = spaceshipsManager.TryToSpawnSpaceship(CurrentPlayerIndex, spaceship, this);
        bool spawned = column != -1;

        if (spawned) {
            int spaceshipIndex = unlockedCards.GetCardStates().FindIndex(x => x.card.Spaceship == spaceship);
            networkTransmitter.SyncSpaceshipSpawningServerRpc(spaceshipIndex, column);
        }

        return spawned;
    }

    public void SyncSpaceshipSpawning(int spaceshipIndex, int column) {
        Spaceship spaceship = unlockedCards.GetCardStates()[spaceshipIndex].card.Spaceship;
        spaceshipsManager.SpawnSpaceship(CurrentPlayerIndex, spaceship, this, column);
    }

    public void SetBattlefieldColumnsActive(bool active) {
        spaceshipsManager.SetBattlefieldColumnsActive(CurrentPlayerIndex, active);
    }

    public void EndTurn() {
        if (spaceshipsManager.IsFirstRowCaptured(CurrentPlayerIndex)) {
            Debug.Log($"Player #{(CurrentPlayerIndex + 1) % 2} won");

            unlockedCards.PlayerCoins += winReward;

            SceneManager.LoadScene("Menu");
            return;
        }

        spaceshipsManager.SetActionAvailableToSpaceships(CurrentPlayerIndex, false);
        int nextPlayerIndex = (CurrentPlayerIndex + 1) % 2;
        spaceshipsManager.SetActionAvailableToSpaceships(nextPlayerIndex, true);

        spaceshipsManager.MoveSpaceshipsForward(nextPlayerIndex);

        uiManager.SetCardsDraggable(false);
        uiManager.SetNextTurnButtonActive(false);

        CurrentPlayerIndex = nextPlayerIndex;
        networkTransmitter.SyncTurnEndingServerRpc();
    }

    public void SyncTurnEnding() {
        spaceshipsManager.SetActionAvailableToSpaceships((CurrentPlayerIndex + 1) % 2, false);
        spaceshipsManager.SetActionAvailableToSpaceships(CurrentPlayerIndex, true);

        spaceshipsManager.MoveSpaceshipsForward(CurrentPlayerIndex);

        uiManager.SetCardsDraggable(true);
        uiManager.SetNextTurnButtonActive(true);
        uiManager.PullCard();
    }

    public void AttackSpaceship(Spaceship attacker, Spaceship target) {
        int attackerIndex = spaceshipsManager.GetSpaceshipIndex(attacker, CurrentPlayerIndex);
        int targetIndex = spaceshipsManager.GetSpaceshipIndex(target, (CurrentPlayerIndex + 1) % 2);
        spaceshipsManager.AttackSpaceship(attacker, target, CurrentPlayerIndex);
        networkTransmitter.SyncSpaceshipAttackingServerRpc(attackerIndex, targetIndex);
    }

    public void SyncSpaceshipAttacking(int attackerIndex, int targetIndex) {
        spaceshipsManager.AttackSpaceship(attackerIndex, targetIndex, CurrentPlayerIndex);
    }

    public void EliminateSpaceship(Spaceship caster, Spaceship target) {
        int casterIndex = spaceshipsManager.GetSpaceshipIndex(caster, CurrentPlayerIndex);
        int targetIndex = spaceshipsManager.GetSpaceshipIndex(target, (CurrentPlayerIndex + 1) % 2);
        spaceshipsManager.EliminateSpaceship(target, CurrentPlayerIndex);
        networkTransmitter.SyncSpaceshipEliminatingServerRpc(casterIndex , targetIndex);
    }

    public void SyncSpaceshipEliminating(int casterIndex, int targetIndex) {
        spaceshipsManager.EliminateSpaceship(targetIndex, CurrentPlayerIndex);
        spaceshipsManager.SpendSpaceshipSkillAction(casterIndex, CurrentPlayerIndex);
    }

    public bool TryToMoveSpaceshipForward(Spaceship spaceship) {
        bool moved = spaceshipsManager.TryToMoveSpaceshipForward(CurrentPlayerIndex, spaceship);

        if (moved) {
            int spaceshipIndex = spaceshipsManager.GetSpaceshipIndex(spaceship, CurrentPlayerIndex);
            networkTransmitter.SyncSpaceshipForwardMovingServerRpc(spaceshipIndex);
        }

        return moved;
    }

    public void SyncSpaceshipForwardMoving(int spaceshipIndex) {
        spaceshipsManager.MoveSpaceshipForward(CurrentPlayerIndex, spaceshipIndex);
        spaceshipsManager.SpendSpaceshipSkillAction(spaceshipIndex, CurrentPlayerIndex);
    }

    public void CancelBattle() {
        SceneManager.LoadScene("Menu");
    }
}
