using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    private CollectionManager collectionManager;

    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI healthPointsText;
    [SerializeField]
    private TextMeshProUGUI damageText;

    [SerializeField]
    private int healthPoints = 2;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private string description;

    private ScrollRect scrollSpace;
    private RectTransform rt;
    private Sprite initialSprite;

    private bool scrolling = false;
    private Vector2 dragStartingPosition;
    private float scrollStartingThreshold;

    private bool unlocked = true;

    private void Awake() {
        rt = GetComponent<RectTransform>();
        scrollSpace = transform.parent.parent.parent.GetComponent<ScrollRect>();
        initialSprite = image.sprite;

        healthPointsText.SetText(healthPoints.ToString());
        damageText.SetText(damage.ToString());
    }

    private void Start() {
        scrollStartingThreshold = rt.rect.width * 0.1f;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        dragStartingPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData) {
        if (scrolling) {
            scrollSpace.OnDrag(eventData);
        } else if (Mathf.Abs(eventData.position.x - dragStartingPosition.x) > scrollStartingThreshold) {
            scrolling = true;
            scrollSpace.OnBeginDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (scrolling) {
            scrolling = false;
            scrollSpace.OnEndDrag(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!scrolling && unlocked) {
            collectionManager.ShowCardInfo(image.sprite, healthPoints, damage, description);
        }
    }

    public void Lock(Sprite lockedCardSprite) {
        unlocked = false;
        image.sprite = lockedCardSprite;
        healthPointsText.SetText("?");
        damageText.SetText("?");
    }

    public void Unlock() {
        unlocked = true;
        image.sprite = initialSprite;
        healthPointsText.SetText(healthPoints.ToString());
        damageText.SetText(damage.ToString());
    }
}
