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

    public int TryToSpawnSpaceship(int playerIndex, Spaceship spaceship, GameManager gameManager) {
        var selectedColumn = battlefield.CheckColumnSelection();
        if (selectedColumn != -1) {
            var spawnedSpaceship = battlefield.SpawnSpaceship(playerIndex, spaceship, false, selectedColumn);
            spawnedSpaceship.Skill.GameManager = gameManager;
            spaceships[playerIndex].Add(spawnedSpaceship);
        }
        return selectedColumn;
    }

    public void SpawnSpaceship(int playerIndex, Spaceship spaceship, GameManager gameManager, int column) {
        var spawnedSpaceship = battlefield.SpawnSpaceship(playerIndex, spaceship, true, column);
        spawnedSpaceship.Skill.GameManager = gameManager;
        spaceships[playerIndex].Add(spawnedSpaceship);
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

    public bool TryToMoveSpaceshipForward(int playerIndex, Spaceship spaceship) {
        return battlefield.TryToMoveSpaceshipForward(playerIndex, spaceship);
    }

    public void MoveSpaceshipForward(int playerIndex, int spaceshipIndex) {
        Spaceship spaceship = spaceships[playerIndex][spaceshipIndex];
        TryToMoveSpaceshipForward(playerIndex, spaceship);
    }

    public void AttackSpaceship(Spaceship attacker, Spaceship target, int playerIndex) {
        target.HealthPoints -= attacker.Card.Damage;
        attacker.ActionAvailable = false;
        if (target.HealthPoints <= 0) {
            EliminateSpaceship(target, playerIndex);
        }
    }

    public void AttackSpaceship(int attackerIndex, int targetIndex, int playerIndex) {
        Spaceship attacker = spaceships[playerIndex][attackerIndex];
        Spaceship target = spaceships[(playerIndex + 1) % 2][targetIndex];
        AttackSpaceship(attacker, target, playerIndex);
    }

    public void EliminateSpaceship(Spaceship target, int playerIndex) {
        target.cell.DetachSpaceship();
        spaceships[(playerIndex + 1) % 2].Remove(target);
        Destroy(target.gameObject);
    }

    public void EliminateSpaceship(int spaceshipIndex, int playerIndex) {
        Spaceship target = spaceships[(playerIndex + 1) % 2][spaceshipIndex];
        EliminateSpaceship(target, playerIndex);
    }

    public bool IsFirstRowCaptured(int playerIndex) {
        return battlefield.IsFirstRowCaptured(playerIndex);
    }

    public void SpendSpaceshipSkillAction(int spaceshipIndex, int playerIndex) {
        Spaceship spaceship = spaceships[playerIndex][spaceshipIndex];
        spaceship.Skill.SpendAction();
    }

    public int GetSpaceshipIndex(Spaceship spaceship, int playerIndex) {
        return spaceships[playerIndex].IndexOf(spaceship);
    }
}
