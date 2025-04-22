using TMPro;
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
    private TextMeshProUGUI cardInfoHealthpoints;
    [SerializeField]
    private TextMeshProUGUI cardInfoDamage;
    [SerializeField]
    private ClickHandler clickHandler;
    [SerializeField]
    private GameObject clickBlock;

    private bool spaceshipSelected = false;

    public void SetNextTurnButtonActive(bool active) {
        nextTurnButton.SetActive(active);
    }

    public void ShowCardInfo(Sprite sprite, int healthpoints, int damage) {
        cardInfo.sprite = sprite;
        cardInfoHealthpoints.SetText(healthpoints.ToString());
        cardInfoDamage.SetText(damage.ToString());

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
        cardInfoHealthpoints.SetText(spaceship.HealthPoints.ToString());
        cardInfoDamage.SetText(spaceship.Card.Damage.ToString());

        cardInfo.GameObject().SetActive(true);
        SetNextTurnButtonActive(false);
    }

    public void HideSpaceshipInfo() {
        cardInfo.GameObject().SetActive(false);
        if (!spaceshipSelected) {
            SetNextTurnButtonActive(true);
        }
    }

    public void SelectAllySpaceship(Spaceship spaceship) {
        cardsScrollSpace.SetActive(false);
        spaceshipsCardsContainer.gameObject.SetActive(true);
        spaceshipsCardsContainer.ShowAllySpaceshipCard(spaceship);
        SetNextTurnButtonActive(false);
        spaceshipSelected = true;
    }

    public void SelectEnemySpaceship(Spaceship spaceship) {
        spaceshipsCardsContainer.ShowEnemySpaceshipCard(spaceship);
    }

    public void UnselectEnemySpaceship() {
        spaceshipsCardsContainer.HideEnemySpaceshipCard();
    }

    public void UnselectSpaceships() {
        cardsScrollSpace.SetActive(true);
        spaceshipsCardsContainer.HideEnemySpaceshipCard();
        spaceshipsCardsContainer.gameObject.SetActive(false);
        SetNextTurnButtonActive(true);
        spaceshipSelected = false;
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
