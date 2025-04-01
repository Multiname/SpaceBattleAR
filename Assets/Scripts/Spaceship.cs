using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Image cardImage;
    private CardInfoContainer cardInfoContainer;

    [HideInInspector]
    public int playerIndex = 0;

    private float holdDuration = 1.0f;
    private float currentHoldDuration = 0.0f;

    private void Start() {
        meshRenderer.material.color = Color.green;
        cardInfoContainer = GameObject.FindGameObjectWithTag("CardInfoContainer").GetComponent<CardInfoContainer>();
    }

    public void SetFriendliness(bool friendly) {
        if (friendly) {
             meshRenderer.material.color = Color.green;
        } else {
            meshRenderer.material.color = Color.red;
        }
    }

    private void OnMouseDrag() {
        if(!EventSystem.current.IsPointerOverGameObject()) {
            currentHoldDuration += Time.deltaTime;
            if (currentHoldDuration >= holdDuration) {
                currentHoldDuration = 0.0f;
                cardInfoContainer.ShowCardInfo(cardImage.sprite);
            }
        }
    }

    private void OnMouseExit() {
        currentHoldDuration = 0.0f;
    }

    private void OnMouseUp() {
        currentHoldDuration = 0.0f;
    }
}
