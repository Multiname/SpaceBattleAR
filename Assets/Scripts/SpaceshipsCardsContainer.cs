using UnityEngine;
using UnityEngine.UI;

public class SpaceshipsCardsContainer : MonoBehaviour
{
    [SerializeField]
    private Image allySpaceshipCard;
    [SerializeField]
    private Image enemySpaceshipCard;
    [SerializeField]
    private Button useSkillButton;

    public void ShowAllySpaceshipCard(Sprite sprite) {
        allySpaceshipCard.sprite = sprite;
    }

    public void ShowEnemySpaceshipCard(Sprite sprite) {
        enemySpaceshipCard.sprite = sprite;
        enemySpaceshipCard.gameObject.SetActive(true);
        useSkillButton.gameObject.SetActive(true);
    }

    public void HideEnemySpaceshipCard() {
        enemySpaceshipCard.gameObject.SetActive(false);
        useSkillButton.gameObject.SetActive(false);
    }

    public void Hide() {
        enemySpaceshipCard.gameObject.SetActive(false);
        useSkillButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
