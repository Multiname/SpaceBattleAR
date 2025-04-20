using System.Collections.Generic;
using UnityEngine;

public class CardsContainer : MonoBehaviour
{
    [SerializeField]
    private Card card;
    [SerializeField]
    private UiManager uiManager;

    private RectTransform rt;

    private List<Card>[] cards = new List<Card>[2] { new(), new() };
    private float scrollSpaceWidth;
    private float cardWidth = 0;
    private int currentPlayerIndex = 0;

    private void Start() {
        rt = GetComponent<RectTransform>();

        scrollSpaceWidth = transform.parent.GetComponent<RectTransform>().rect.width;

        rt.sizeDelta = new Vector2(scrollSpaceWidth, rt.sizeDelta.y);
    }

    public void SwitchToCurrentPlayer(int playerIndex) {
        currentPlayerIndex = playerIndex;
        foreach (var card in cards[(currentPlayerIndex + 1) % 2]) {
            card.gameObject.SetActive(false);
        }

        foreach(var card in cards[currentPlayerIndex]) {
            card.gameObject.SetActive(true);
        }

        AdaptContainerWidth();
    }

    public void PullCard() {
        var newCard = Instantiate(card, transform.GetChild(0));
        newCard.cardsContainer = this;

        if (cardWidth == 0) {
            cardWidth = newCard.Width;
        }

        MoveCardAtPosition(newCard, cards[currentPlayerIndex].Count);

        newCard.SetHealthpointsText(newCard.HealthPoints);
        newCard.SetDamageText(newCard.Damage);

        cards[currentPlayerIndex].Add(newCard);

        AdaptContainerWidth();
    }

    private void RemoveCard(Card card) {
        for (var i = cards[currentPlayerIndex].IndexOf(card) + 1; i < cards[currentPlayerIndex].Count; ++i) {
            MoveCardAtPosition(cards[currentPlayerIndex][i], i - 1);
        }
        cards[currentPlayerIndex].Remove(card);
        AdaptContainerWidth();
        Destroy(card.gameObject);
    }

    private void MoveCardAtPosition(Card card, int positionIndex) {
        var position = card.rt.anchoredPosition;
        position.x = (cardWidth + 50) * positionIndex;
        card.rt.anchoredPosition = position;
    }

    private void AdaptContainerWidth() {
        var containerWidth = (cardWidth + 50) * cards[currentPlayerIndex].Count + 50;
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

    public void ShowCardInfo(Sprite sprite, int healthpoints, int damage) {
        uiManager.ShowCardInfo(sprite, healthpoints, damage);
    }
}
