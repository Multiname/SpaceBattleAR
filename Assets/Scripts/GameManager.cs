using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace System.Runtime.CompilerServices {
        internal static class IsExternalInit {}
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Battlefield battlefieldPrefab;
    [SerializeField]
    private CardsContainer cardsContainer;
    
    [SerializeField] // DEBUG
    private Battlefield battlefield;
    private List<Spaceship>[] spaceships = new List<Spaceship>[2] { new(), new() };

    public int CurrentPlayerIndex { get; private set; } = 0;

    private void Start() {
        cardsContainer.PullCard();
        cardsContainer.PullCard();
    }

    public void CreateBattlefield(GameObject origin) {
        battlefield = Instantiate(battlefieldPrefab, origin.transform);
        cardsContainer.PullCard();
    }

    public bool TryToSpawnSpaceship(Spaceship spaceship) {
        var selectedColumn = battlefield.CheckColumnSelection();
        if (selectedColumn != -1) {
            var spawnedSpaceship = battlefield.SpawnSpaceship(spaceship, CurrentPlayerIndex, selectedColumn);
            spaceships[CurrentPlayerIndex].Add(spawnedSpaceship);
            return true;
        }
        return false;
    }

    public void SetBattlefieldColumnsActive(bool active) {
        battlefield.SetColumnsActive(CurrentPlayerIndex, active);
    }

    public void EndTurn() {
        CurrentPlayerIndex = ++CurrentPlayerIndex % 2;

        bool firstPlayerisNext = CurrentPlayerIndex == 0;
        foreach (var spaceship in spaceships[0]) {
            spaceship.SetFriendliness(firstPlayerisNext);
        }
        foreach (var spaceship in spaceships[1]) {
            spaceship.SetFriendliness(!firstPlayerisNext);
        }

        cardsContainer.SwitchToCurrentPlayer();
        cardsContainer.PullCard();
    }
}
