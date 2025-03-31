using System.Collections.Generic;
using UnityEngine;

public class CardsContainer : MonoBehaviour
{
    [SerializeField]
    private Card card;
    private GameManager gameManager;

    private RectTransform rt;

    private List<Card>[] cards = new List<Card>[2] { new(), new() };
    private float scrollSpaceWidth;
    private float cardWidth = 0;

    private void Start() {
        rt = GetComponent<RectTransform>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        scrollSpaceWidth = transform.parent.GetComponent<RectTransform>().rect.width;

        rt.sizeDelta = new Vector2(scrollSpaceWidth, rt.sizeDelta.y);
    }

    public void SwitchToCurrentPlayer() {
        foreach (var card in cards[(gameManager.CurrentPlayerIndex + 1) % 2]) {
            card.gameObject.SetActive(false);
        }

        foreach(var card in cards[gameManager.CurrentPlayerIndex]) {
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

        MoveCardAtPosition(newCard, cards[gameManager.CurrentPlayerIndex].Count);

        cards[gameManager.CurrentPlayerIndex].Add(newCard);

        AdaptContainerWidth();
    }

    private void RemoveCard(Card card) {
        for (var i = cards[gameManager.CurrentPlayerIndex].IndexOf(card) + 1; i < cards[gameManager.CurrentPlayerIndex].Count; ++i) {
            MoveCardAtPosition(cards[gameManager.CurrentPlayerIndex][i], i - 1);
        }
        cards[gameManager.CurrentPlayerIndex].Remove(card);
        AdaptContainerWidth();
        Destroy(card.gameObject);
    }

    private void MoveCardAtPosition(Card card, int positionIndex) {
        var position = card.rt.anchoredPosition;
        position.x = (cardWidth + 50) * positionIndex;
        card.rt.anchoredPosition = position;
    }

    private void AdaptContainerWidth() {
        var containerWidth = (cardWidth + 50) * cards[gameManager.CurrentPlayerIndex].Count + 50;
        if (containerWidth > scrollSpaceWidth) {
            rt.sizeDelta = new Vector2(containerWidth, rt.sizeDelta.y);
        }
    }

    public void HandleCardDragBeginning() {
        gameManager.SetBattlefieldColumnsActive(true);
    }

    public void HandleCardDragEnding(Card card) {
        var spawned = gameManager.TryToSpawnSpaceship(card.Spaceship);
        gameManager.SetBattlefieldColumnsActive(false);

        if (spawned) {
            RemoveCard(card);
        }
    }
}
