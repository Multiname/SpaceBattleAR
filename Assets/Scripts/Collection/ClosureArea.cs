using UnityEngine;
using UnityEngine.EventSystems;

public class ClosureArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private CollectionManager collectionManager;

    public void OnPointerClick(PointerEventData eventData) {
        collectionManager.HideCardInfo();
    }
}
