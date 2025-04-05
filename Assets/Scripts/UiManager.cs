using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private CardsContainer cardsContainer;
    [SerializeField]
    private SpaceshipsCardsContainer spaceshipsCardsContainer;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject cardsScrollSpace;
    [SerializeField]
    private ClosureArea closureArea;
    [SerializeField]
    private GameObject nextTurnButton;
    [SerializeField]
    private Image cardInfo;

    public void ShowCardInfo(Sprite sprite) {
        cardInfo.sprite = sprite;
        cardInfo.GameObject().SetActive(true);
        nextTurnButton.SetActive(false);
        closureArea.HandleClosure = () => {
            cardInfo.GameObject().SetActive(false);
            nextTurnButton.SetActive(true);
            closureArea.gameObject.SetActive(false);
        };
        closureArea.gameObject.SetActive(true);
    }

    public void ShowSpaceshipInfo(Spaceship spaceship) {
        cardInfo.sprite = spaceship.CardImage.sprite;
        cardInfo.GameObject().SetActive(true);
        nextTurnButton.SetActive(false);
        closureArea.HandleClosure = () => {
            spaceship.Unselect();
            cardInfo.GameObject().SetActive(false);
            nextTurnButton.SetActive(true);
            closureArea.gameObject.SetActive(false);
        };
        closureArea.gameObject.SetActive(true);
    }

    public void SelectSpaceship(Spaceship spaceship) {
        cardsScrollSpace.SetActive(false);
        spaceshipsCardsContainer.gameObject.SetActive(true);
        spaceshipsCardsContainer.ShowAllySpaceshipCard(spaceship.CardImage.sprite);
        closureArea.HandleClosure = () => {
            spaceship.Unselect();
            cardsScrollSpace.SetActive(true);
            spaceshipsCardsContainer.gameObject.SetActive(false);
            closureArea.gameObject.SetActive(false);
        };
        closureArea.gameObject.SetActive(true);
    }

    public void PullCard() {
        cardsContainer.PullCard();
    }

    public void SwitchToCurrentPlayersCards(int playerIndex) {
        cardsContainer.SwitchToCurrentPlayer(playerIndex);
    }

    public void HandleCardDragBeginning() {
        gameManager.SetBattlefieldColumnsActive(true);
    }

    public bool HandleCardDragEnding(Spaceship spaceship) {
        var spawned = gameManager.TryToSpawnSpaceship(spaceship);
        gameManager.SetBattlefieldColumnsActive(false);
        return spawned;
    }
}
