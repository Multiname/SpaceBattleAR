using System.Collections.Generic;
using UnityEngine;

public class CardsContainer : MonoBehaviour
{
    [SerializeField]
    private Card card;
    private GameManager gameManager;

    private RectTransform rt;

    private List<Card> cards = new();
    private float scrollSpaceWidth;

    private void Start() {
        rt = GetComponent<RectTransform>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        scrollSpaceWidth = transform.parent.GetComponent<RectTransform>().rect.width;

        rt.sizeDelta = new Vector2(scrollSpaceWidth, rt.sizeDelta.y);
    }

    public void GetCard() {
        var newCard = Instantiate(card, transform.GetChild(0));
        newCard.cardsContainer = this;

        MoveCardAtPosition(newCard, cards.Count);

        cards.Add(newCard);

        AdaptContainerWidth(newCard.Width);
    }

    private void RemoveCard(Card card) {
        for (var i = cards.IndexOf(card) + 1; i < cards.Count; ++i) {
            MoveCardAtPosition(cards[i], i - 1);
        }
        cards.Remove(card);
        AdaptContainerWidth(card.Width);
        Destroy(card.gameObject);
    }

    private void MoveCardAtPosition(Card card, int positionIndex) {
        var position = card.transform.position;
        position.x = 50 + (card.Width + 50) * positionIndex;
        card.transform.position = position;
    }

    private void AdaptContainerWidth(float cardWidth) {
        var containerWidth = (cardWidth + 50) * cards.Count + 50;
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
