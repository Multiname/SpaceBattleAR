using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField]
    private GameObject columns;
    [SerializeField]
    private GameObject rows;

    private BattlefieldColumn[] battlefieldColumns;
    private Cell[][] cells;

    private void Start() {
        battlefieldColumns = columns.GetComponentsInChildren<BattlefieldColumn>(true);

        var rowsNumber = rows.transform.childCount;
        cells = new Cell[rowsNumber][];
        for (var i = 0; i < rowsNumber; ++i) {
            cells[i] = rows.transform.GetChild(i).GetComponentsInChildren<Cell>();
        }
    }

    public int CheckColumnSelection() {
        for (var i = 0; i < battlefieldColumns.Length; ++i) {
            if (battlefieldColumns[i].Selected) {
                return i;
            }
        }
        return -1;
    }

    public void SetColumnsActive(int playerIndex, bool active) {
        int playerRow = 0;
        if (playerIndex == 1) {
            playerRow = cells.Length - 1;
        }

        for (var i = 0; i < cells[playerRow].Length; ++i) {
            if (cells[playerRow][i].IsOccupied()) {
                battlefieldColumns[i].occupied = true;
            }
        }

        battlefieldColumns[0].transform.parent.gameObject.SetActive(active);
    }

    public Spaceship SpawnSpaceship(Spaceship spaceship, int playerIndex, int column, int row = 0) {
        if (playerIndex == 1) {
            row = cells.Length - 1 - row;
        }

        if (!cells[row][column].IsOccupied()) {
            return cells[row][column].PlaceSpaceship(spaceship, playerIndex);
        }
        return null;
    }

    public void TryToMoveSpaceshipForward(int playerIndex, Spaceship spaceship) {
        int directionMultiplier = 1;
        if (playerIndex == 1) {
            directionMultiplier = -1;
        }
        
        var nextRow = spaceship.cell.Row + directionMultiplier;
        if (nextRow < cells.Length && nextRow >= 0) {
            var nextCell = cells[nextRow][spaceship.cell.Column];
            if (!nextCell.IsOccupied()) {
                spaceship.cell.DetachSpaceship();
                nextCell.AttachSpaceship(spaceship);
            }
        }
    }
}
