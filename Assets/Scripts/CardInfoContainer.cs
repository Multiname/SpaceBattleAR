using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoContainer : MonoBehaviour
{
    [SerializeField]
    private Image cardInfo;
    [SerializeField]
    private GameObject cardInfoClosureArea;
    [SerializeField]
    private GameObject nextTurnButton;

    public void ShowCardInfo(Sprite cardInfo) {
        this.cardInfo.sprite = cardInfo;
        this.cardInfo.GameObject().SetActive(true);
        nextTurnButton.SetActive(false);
        cardInfoClosureArea.SetActive(true);
    }

    public void HideCardInfo() {
        cardInfo.GameObject().SetActive(false);
        nextTurnButton.SetActive(true);
        cardInfoClosureArea.SetActive(false);
    }
}
