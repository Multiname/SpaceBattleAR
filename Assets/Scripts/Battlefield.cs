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

    public void SetColumnsActive(bool active) {
        battlefieldColumns[0].transform.parent.gameObject.SetActive(active);
    }

    public GameObject SpawnSpaceship(GameObject spaceship, int column, int row = 0) {
        if (!cells[row][column].IsOccupied()) {
            if (row == 0) {
                battlefieldColumns[column].occupied = true;
            }
            return cells[row][column].PlaceSpaceship(spaceship);
        }
        return null;
    }
}
