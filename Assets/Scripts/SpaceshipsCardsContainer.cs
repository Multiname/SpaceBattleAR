using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipsCardsContainer : MonoBehaviour
{
    [SerializeField]
    private Image allySpaceshipCard;
    [SerializeField]
    private Image enemySpaceshipCard;
    [SerializeField]
    private GameObject useSkillButton;
    [SerializeField]
    private TextMeshProUGUI allySpaceshipCardHealthpoints;
    [SerializeField]
    private TextMeshProUGUI allySpaceshipCardDamage;
    [SerializeField]
    private GameObject allySpaceshipCardSkillUnavailableOverlay;
    [SerializeField]
    private TextMeshProUGUI enemySpaceshipCardHealthpoints;
    [SerializeField]
    private TextMeshProUGUI enemySpaceshipCardDamage;

    public void ShowAllySpaceshipCard(Spaceship spaceship) {
        allySpaceshipCard.sprite = spaceship.Card.Image.sprite;
        allySpaceshipCardHealthpoints.SetText(spaceship.HealthPoints.ToString());
        allySpaceshipCardDamage.SetText(spaceship.Card.Damage.ToString());

        allySpaceshipCardSkillUnavailableOverlay.SetActive(!spaceship.Skill.Available);
        useSkillButton.SetActive(spaceship.Skill.Available);
    }

    public void ShowEnemySpaceshipCard(Spaceship spaceship) {
        enemySpaceshipCard.gameObject.SetActive(true);
        
        enemySpaceshipCard.sprite = spaceship.Card.Image.sprite;
        enemySpaceshipCardHealthpoints.SetText(spaceship.HealthPoints.ToString());
        enemySpaceshipCardDamage.SetText(spaceship.Card.Damage.ToString());
    }

    public void HideEnemySpaceshipCard() {
        enemySpaceshipCard.gameObject.SetActive(false);
    }

    public void Hide() {
        enemySpaceshipCard.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
