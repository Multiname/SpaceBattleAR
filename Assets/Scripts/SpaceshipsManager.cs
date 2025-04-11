using System.Collections.Generic;
using UnityEngine;

public class SpaceshipsManager : MonoBehaviour
{
    [SerializeField]
    private Battlefield battlefieldPrefab;

    [SerializeField] // DEBUG
    private Battlefield battlefield;
    private List<Spaceship>[] spaceships = new List<Spaceship>[2] { new(), new() };

    public void CreateBattlefield(GameObject origin) {
        battlefield = Instantiate(battlefieldPrefab, origin.transform);
    }

    public void SetBattlefieldColumnsActive(int playerIndex, bool active) {
        battlefield.SetColumnsActive(playerIndex, active);
    }

    public bool TryToSpawnSpaceship(int playerIndex, Spaceship spaceship) {
        var selectedColumn = battlefield.CheckColumnSelection();
        if (selectedColumn != -1) {
            var spawnedSpaceship = battlefield.SpawnSpaceship(spaceship, playerIndex, selectedColumn);
            spaceships[playerIndex].Add(spawnedSpaceship);
            return true;
        }
        return false;
    }

    public void SetSpaceshipsFriendliness(bool firstPlayerisNext) {
        foreach (var spaceship in spaceships[0]) {
            spaceship.SetFriendliness(firstPlayerisNext);
        }
        foreach (var spaceship in spaceships[1]) {
            spaceship.SetFriendliness(!firstPlayerisNext);
        }
    }
}
