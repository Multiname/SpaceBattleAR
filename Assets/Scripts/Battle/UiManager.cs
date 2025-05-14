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
    private GameObject topButtons;
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

    public void SetTopButtonsActive(bool active) {
        topButtons.SetActive(active);
    }

    public void SetNextTurnButtonActive(bool active) {
        nextTurnButton.SetActive(active);
    }

    public void ShowCardInfo(Sprite sprite, int healthpoints, int damage) {
        cardInfo.sprite = sprite;
        cardInfoHealthpoints.SetText(healthpoints.ToString());
        cardInfoDamage.SetText(damage.ToString());

        cardInfo.GameObject().SetActive(true);
        SetTopButtonsActive(false);
        clickBlock.SetActive(true);
        clickHandler.SetCardInfoShown();
    }

    public void HideCardInfo() {
        cardInfo.GameObject().SetActive(false);
        clickBlock.SetActive(false);
        if (!spaceshipSelected) {
            SetTopButtonsActive(true);
        }
    }

    public void ShowSpaceshipInfo(Spaceship spaceship) {
        cardInfo.sprite = spaceship.Card.Image.sprite;
        cardInfoHealthpoints.SetText(spaceship.HealthPoints.ToString());
        cardInfoDamage.SetText(spaceship.Card.Damage.ToString());

        cardInfo.GameObject().SetActive(true);
        SetTopButtonsActive(false);
    }

    public void HideSpaceshipInfo() {
        cardInfo.GameObject().SetActive(false);
        if (!spaceshipSelected) {
            SetTopButtonsActive(true);
        }
    }

    public void SelectAllySpaceship(Spaceship spaceship) {
        cardsScrollSpace.SetActive(false);
        spaceshipsCardsContainer.gameObject.SetActive(true);
        spaceshipsCardsContainer.ShowAllySpaceshipCard(spaceship);
        SetTopButtonsActive(false);
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
        spaceshipsCardsContainer.Hide();
        spaceshipsCardsContainer.gameObject.SetActive(false);
        SetTopButtonsActive(true);
        spaceshipSelected = false;
    }

    public void PullCard() {
        cardsContainer.PullCard();
    }

    public void SetCardsDraggable(bool draggable) {
        cardsContainer.SetCardsDraggable(draggable);
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
