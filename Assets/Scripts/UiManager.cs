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
    private GameObject nextTurnButton;
    [SerializeField]
    private Image cardInfo;
    [SerializeField]
    private ClickHandler clickHandler;
    [SerializeField]
    private GameObject clickBlock;

    private bool spaceshipSelected = false;

    public void SetNextTurnButtonActive(bool active) {
        nextTurnButton.SetActive(active);
    }

    public void ShowCardInfo(Sprite sprite) {
        cardInfo.sprite = sprite;
        cardInfo.GameObject().SetActive(true);
        SetNextTurnButtonActive(false);
        clickBlock.SetActive(true);
        clickHandler.SetCardInfoShown();
    }

    public void HideCardInfo() {
        cardInfo.GameObject().SetActive(false);
        clickBlock.SetActive(false);
        if (!spaceshipSelected) {
            SetNextTurnButtonActive(true);
        }
    }

    public void ShowSpaceshipInfo(Spaceship spaceship) {
        cardInfo.sprite = spaceship.Card.Image.sprite;
        cardInfo.GameObject().SetActive(true);
        SetNextTurnButtonActive(false);
    }

    public void HideSpaceshipInfo() {
        cardInfo.GameObject().SetActive(false);
        if (!spaceshipSelected) {
            SetNextTurnButtonActive(true);
        }
    }

    public void SelectAllySpaceship(Sprite sprite) {
        cardsScrollSpace.SetActive(false);
        spaceshipsCardsContainer.gameObject.SetActive(true);
        spaceshipsCardsContainer.ShowAllySpaceshipCard(sprite);
        SetNextTurnButtonActive(false);
        spaceshipSelected = true;
    }

    public void SelectEnemySpaceship(Sprite sprite) {
        spaceshipsCardsContainer.ShowEnemySpaceshipCard(sprite);
    }

    public void UnselectEnemySpaceship() {
        spaceshipsCardsContainer.HideEnemySpaceshipCard();
    }

    public void UnselectSpaceships() {
        cardsScrollSpace.SetActive(true);
        spaceshipsCardsContainer.gameObject.SetActive(false);
        SetNextTurnButtonActive(true);
        spaceshipSelected = false;
    }

    public bool TryToSelectTargetSpaceship(Sprite sprite) {
        if (spaceshipsCardsContainer.gameObject.activeSelf) {
            spaceshipsCardsContainer.ShowEnemySpaceshipCard(sprite);
        }
        return true;
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

    public void AttackSpaceship(Spaceship attacker, Spaceship target) {
        gameManager.AttackSpaceship(attacker, target);
    }
}
