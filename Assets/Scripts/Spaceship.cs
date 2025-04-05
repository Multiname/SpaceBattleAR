using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [field: SerializeField]
    public Image CardImage { get; private set; }
    public SpaceshipsManager SpaceshipsManager { private get; set; }
    
    private Color teamColor;
    private bool selected = false;
    private bool mouseOverSpaceship = false;

    [HideInInspector]
    public int playerIndex = 0;

    private float holdDuration = 1.0f;

    private void Start() {
        meshRenderer.material.color = Color.green;
        teamColor = Color.green;
    }

    public void SetFriendliness(bool friendly) {
        if (friendly) {
             meshRenderer.material.color = Color.green;
             teamColor = Color.green;
        } else {
            meshRenderer.material.color = Color.red;
            teamColor = Color.red;
        }
    }

    private void Select() {
        meshRenderer.material.color = Color.blue;
        selected = true;
    }

    public void Unselect() {
        meshRenderer.material.color = teamColor;
        selected = false;
    }

    private void OnMouseEnter() {
        mouseOverSpaceship = true;
    }

    private void OnMouseExit() {
        mouseOverSpaceship = false;
    }

    private void OnMouseDown() {
        if(!EventSystem.current.IsPointerOverGameObject()) {
            Invoke(nameof(OnLongPress), holdDuration);
        }
    }

    private void OnLongPress() {
        Select();
        SpaceshipsManager.ShowSpaceshipInfo(this);
    }

    private void OnMouseUp() {
        if (!selected) {
            CancelInvoke(nameof(OnLongPress));
            if (!EventSystem.current.IsPointerOverGameObject() && 
                                mouseOverSpaceship &&
                                SpaceshipsManager.GetCurrentPlayerIndex() == playerIndex) {
                Select();
                SpaceshipsManager.SelectSpaceship(this);
            }
        }
    }
}
