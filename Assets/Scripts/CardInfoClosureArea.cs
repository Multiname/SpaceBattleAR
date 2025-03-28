using UnityEngine;
using UnityEngine.EventSystems;

public class CardInfoClosureArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private CardInfoContainer cardInfoContainer;

    public void OnPointerClick(PointerEventData eventData) {
        cardInfoContainer.HideCardInfo();
    }
}
