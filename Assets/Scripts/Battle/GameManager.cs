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
        get => networkTransmitter.currentPlayerIndex.Value;
        set => networkTransmitter.SetCurrentPlayerInderServerRpc(value);
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
            uiManager.SetTopButtonsActive(true);
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
        CurrentPlayerIndex = ++CurrentPlayerIndex % 2;
        spaceshipsManager.SetActionAvailableToSpaceships(CurrentPlayerIndex, true);

        bool firstPlayerisNext = CurrentPlayerIndex == 0;
        spaceshipsManager.SetSpaceshipsFriendliness(firstPlayerisNext);

        spaceshipsManager.MoveSpaceshipsForward(CurrentPlayerIndex);

        uiManager.SwitchToCurrentPlayersCards(CurrentPlayerIndex);
        uiManager.PullCard();
    }

    public void AttackSpaceship(Spaceship attacker, Spaceship target) {
        spaceshipsManager.AttackSpaceship(attacker, target, CurrentPlayerIndex);
    }

    public void EliminateSpaceship(Spaceship target) {
        spaceshipsManager.EliminateSpaceship(target, CurrentPlayerIndex);
    }

    public bool TryToMoveSpaceshipForward(Spaceship spaceship) {
        return spaceshipsManager.TryToMoveSpaceshipForward(CurrentPlayerIndex, spaceship);
    }

    public void CancelBattle() {
        SceneManager.LoadScene("Menu");
    }
}
