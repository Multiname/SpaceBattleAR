using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [HideInInspector]
    public RectTransform rt;
    private ScrollRect scrollSpace;
    private Image image;

    [field: SerializeField]
    public Spaceship Spaceship { get; private set; }
    [HideInInspector]
    public CardsContainer cardsContainer;
    [HideInInspector]
    public float Width { get; private set; }

    private Vector2 initialPosition;
    private float dragStartingThreshold;
    private float scrollStartingThreshold;

    private Vector2 dragStartingPosition;
    private bool dragging = false;
    private bool scrolling = false;

    private void Awake() {
        rt = GetComponent<RectTransform>();
        scrollSpace = transform.parent.parent.parent.GetComponent<ScrollRect>();
        image = GetComponent<Image>();

        Width = rt.rect.width;
    }
    
    private void Start() {
        dragStartingThreshold = rt.rect.height * 0.1f;
        scrollStartingThreshold = rt.rect.width * 0.1f;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        dragStartingPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData) {
        if (scrolling) {
            scrollSpace.OnDrag(eventData);
        } else if (dragging) {
            rt.anchoredPosition += eventData.delta;
        } else if (Mathf.Abs(eventData.position.x - dragStartingPosition.x) > scrollStartingThreshold) {
            scrolling = true;
            scrollSpace.OnBeginDrag(eventData);
        } else if (eventData.position.y - dragStartingPosition.y > dragStartingThreshold) {
            dragging = true;
            initialPosition = rt.anchoredPosition;
            transform.SetAsLastSibling();
            cardsContainer.HandleCardDragBeginning();
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (scrolling) {
            scrolling = false;
            scrollSpace.OnEndDrag(eventData);
        } else if (dragging) {
            dragging = false;
            rt.anchoredPosition = initialPosition;
            cardsContainer.HandleCardDragEnding(this);
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!scrolling && !dragging) {
            cardsContainer.ShowCardInfo(image.sprite);
        }
    }
}
