using System.Collections.Generic;
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
    private List<GameObject> spaceships = new();

    private void Start() {
        cardsContainer.GetCard();
        cardsContainer.GetCard();
    }

    public void CreateBattlefield(GameObject origin) {
        battlefield = Instantiate(battlefieldPrefab, origin.transform);
        cardsContainer.GetCard();
    }

    public bool TryToSpawnSpaceship(GameObject spaceship) {
        var selectedColumn = battlefield.CheckColumnSelection();
        if (selectedColumn != -1) {
            var spawnedSpaceship = battlefield.SpawnSpaceship(spaceship, selectedColumn);
            spaceships.Add(spawnedSpaceship);
            return true;
        }
        return false;
    }

    public void SetBattlefieldColumnsActive(bool active) {
        battlefield.SetColumnsActive(active);
    }
}
