using System.Collections.Generic;
using UnityEngine;

public class CardsContainer : MonoBehaviour
{
    [SerializeField]
    private UnlockedCards unlockedCards;
    [SerializeField]
    private UiManager uiManager;

    private RectTransform rt;

    private List<Card> currentCardPool = new();
    private List<Card> cards = new();
    private float scrollSpaceWidth;
    private float cardWidth = 0;

    private void Start() {
        rt = GetComponent<RectTransform>();

        scrollSpaceWidth = transform.parent.GetComponent<RectTransform>().rect.width;

        rt.sizeDelta = new Vector2(scrollSpaceWidth, rt.sizeDelta.y);

        currentCardPool = new(unlockedCards.GetUnlockedCards());
    }

    public void SetCardsDraggable(bool draggable) {
        foreach (var card in cards) {
            card.draggable = draggable;
        }
    }

    public void PullCard() {
        if (currentCardPool.Count == 0) {
            currentCardPool = new(unlockedCards.GetUnlockedCards());
        }

        int cardIndex = Random.Range(0, currentCardPool.Count);
        Card card = currentCardPool[cardIndex];
        currentCardPool.RemoveAt(cardIndex);

        var newCard = Instantiate(card, transform.GetChild(0));
        newCard.cardsContainer = this;

        if (cardWidth == 0) {
            cardWidth = newCard.Width;
        }

        MoveCardAtPosition(newCard, cards.Count);

        newCard.SetHealthpointsText(newCard.HealthPoints);
        newCard.SetDamageText(newCard.Damage);

        cards.Add(newCard);

        AdaptContainerWidth();
    }

    private void RemoveCard(Card card) {
        for (var i = cards.IndexOf(card) + 1; i < cards.Count; ++i) {
            MoveCardAtPosition(cards[i], i - 1);
        }
        cards.Remove(card);
        AdaptContainerWidth();
        Destroy(card.gameObject);
    }

    private void MoveCardAtPosition(Card card, int positionIndex) {
        var position = card.rt.anchoredPosition;
        position.x = (cardWidth + 50) * positionIndex;
        card.rt.anchoredPosition = position;
    }

    private void AdaptContainerWidth() {
        var containerWidth = (cardWidth + 50) * cards.Count + 50;
        if (containerWidth > scrollSpaceWidth) {
            rt.sizeDelta = new Vector2(containerWidth, rt.sizeDelta.y);
        }
    }

    public void HandleCardDragBeginning() {
        uiManager.HandleCardDragBeginning();
    }

    public void HandleCardDragEnding(Card card) {
        var spawned = uiManager.HandleCardDragEnding(card.Spaceship);

        if (spawned) {
            RemoveCard(card);
        }
    }

    public void ShowCardInfo(Card card) {
        uiManager.ShowCardInfo(card);
    }
}
