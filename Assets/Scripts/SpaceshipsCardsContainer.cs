using UnityEngine;
using UnityEngine.UI;

public class SpaceshipsCardsContainer : MonoBehaviour
{
    [SerializeField]
    private Image allySpaceshipCard;
    [SerializeField]
    private Image enemySpaceshipCard;

    public void ShowAllySpaceshipCard(Sprite sprite) {
        allySpaceshipCard.sprite = sprite;
    }

    public void ShowEnemySpaceshipCard(Sprite sprite) {
        enemySpaceshipCard.sprite = sprite;
        enemySpaceshipCard.gameObject.SetActive(true);
    }

    public void Hide() {
        enemySpaceshipCard.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
