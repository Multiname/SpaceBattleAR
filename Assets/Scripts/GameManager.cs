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
    }

    public void CreateBattlefield(GameObject origin) {
        spaceshipsManager.CreateBattlefield(origin);
        uiManager.PullCard();
    }

    public bool TryToSpawnSpaceship(Spaceship spaceship) {
        return spaceshipsManager.TryToSpawnSpaceship(CurrentPlayerIndex, spaceship);
    }

    public void SetBattlefieldColumnsActive(bool active) {
        spaceshipsManager.SetBattlefieldColumnsActive(CurrentPlayerIndex, active);
    }

    public void EndTurn() {
        CurrentPlayerIndex = ++CurrentPlayerIndex % 2;

        bool firstPlayerisNext = CurrentPlayerIndex == 0;
        spaceshipsManager.SetSpaceshipsFriendliness(firstPlayerisNext);

        uiManager.SwitchToCurrentPlayersCards(CurrentPlayerIndex);
        uiManager.PullCard();
    }
}
