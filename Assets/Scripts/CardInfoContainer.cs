using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoContainer : MonoBehaviour
{
    [SerializeField]
    private Image cardInfo;
    [SerializeField]
    private GameObject cardInfoClosureArea;

    public void ShowCardInfo(Sprite cardInfo) {
        this.cardInfo.sprite = cardInfo;
        this.cardInfo.GameObject().SetActive(true);
        cardInfoClosureArea.SetActive(true);
    }

    public void HideCardInfo() {
        cardInfo.GameObject().SetActive(false);
        cardInfoClosureArea.SetActive(false);
    }
}
