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

    public void SetActionAvailableToSpaceships(int index, bool available) {
        foreach (var spaceship in spaceships[index]) {
            spaceship.ActionAvailable = available;
        }
    }

    public void MoveSpaceshipsForward(int index) {
        foreach (var spaceship in spaceships[index]) {
            battlefield.TryToMoveSpaceshipForward(index, spaceship);
        }
    }

    public void AttackSpaceship(Spaceship attacker, Spaceship target, int playerIndex) {
        target.HealthPoints -= attacker.Card.Damage;
        attacker.ActionAvailable = false;
        if (target.HealthPoints <= 0) {
            target.cell.DetachSpaceship();
            spaceships[++playerIndex % 2].Remove(target);
            Destroy(target.gameObject);
        }
    }
}
