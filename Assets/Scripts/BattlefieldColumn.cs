using UnityEngine;

public class BattlefieldColumn : MonoBehaviour
{
    private MeshRenderer mr;

    [SerializeField]
    private Material selectedMaterial;
    [SerializeField]
    private Material unselectedMaterial;
    [SerializeField]
    private Material occupiedMaterial;

    public bool Selected { private set; get; } = false;
    [HideInInspector]
    public bool occupied = false;

    private void Start() {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter() {
        if (occupied) {
            mr.material = occupiedMaterial;
        } else {
            Selected = true;
            mr.material = selectedMaterial;
        }
    }

    private void OnMouseExit() {
        if (!occupied) {
            Selected = false;
            mr.material = unselectedMaterial;
        }
    }

    private void OnDisable() {
        Selected = false;
        occupied = false;
        mr.material = unselectedMaterial;
    }

    private void OnEnable() {
        if (occupied) {
            mr.material = occupiedMaterial;
        }
    }
}
