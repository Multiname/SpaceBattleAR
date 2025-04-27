using UnityEngine;

namespace System.Runtime.CompilerServices {
        internal static class IsExternalInit {}
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UiManager uiManager;
    [SerializeField]
    private SpaceshipsManager spaceshipsManager;
    
    public int CurrentPlayerIndex { get; private set; } = 0;

    private void Start() {
        uiManager.PullCard();
        uiManager.PullCard();
        uiManager.SetNextTurnButtonActive(true);
    }

    public void CreateBattlefield(GameObject origin) {
        spaceshipsManager.CreateBattlefield(origin);
        uiManager.PullCard();
        uiManager.SetNextTurnButtonActive(true);
    }

    public bool TryToSpawnSpaceship(Spaceship spaceship) {
        return spaceshipsManager.TryToSpawnSpaceship(CurrentPlayerIndex, spaceship, this);
    }

    public void SetBattlefieldColumnsActive(bool active) {
        spaceshipsManager.SetBattlefieldColumnsActive(CurrentPlayerIndex, active);
    }

    public void EndTurn() {
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
}
