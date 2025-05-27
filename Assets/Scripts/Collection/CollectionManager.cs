using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    [SerializeField]
    private Image cardInfo;
    [SerializeField]
    private TextMeshProUGUI healthPointsText;
    [SerializeField]
    private TextMeshProUGUI damageText;
    [SerializeField]
    private TextMeshProUGUI cardDescription;

    [SerializeField]
    private GameObject returnToMenuButton;
    [SerializeField]
    private GameObject cardsScrollSpace;
    [SerializeField]
    private List<CollectionCard> cards = new();
    [SerializeField]
    private GameObject bottomPanel;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private GameObject closureArea;

    [SerializeField]
    private UnlockedCards unlockedCards;
    [SerializeField]
    private Sprite lockedCardSprite;

    [SerializeField]
    private int cardPrice;

    private int Coins { 
        get => unlockedCards.playerCoins;
        set {
            coinsText.SetText(value.ToString());
            unlockedCards.playerCoins = value;
        }
    }

    private void Start() {
        coinsText.SetText(Coins.ToString());

        var cardStates = unlockedCards.GetCardStates();
        for (int i = 0; i < cardStates.Count; ++i) {
            if (!cardStates[i].unlocked) {
                cards[i].Lock(lockedCardSprite);
            }
        }
    }

    public void ShowCardInfo(Sprite sprite, int healthPoints, int damage, string description) {
        cardInfo.sprite = sprite;
        cardDescription.SetText(description);
        healthPointsText.SetText(healthPoints.ToString());
        damageText.SetText(damage.ToString());

        cardInfo.gameObject.SetActive(true);
        cardDescription.gameObject.SetActive(true);
        returnToMenuButton.SetActive(false);
        cardsScrollSpace.SetActive(false);
        bottomPanel.SetActive(false);
        closureArea.SetActive(true);
    }

    public void HideCardInfo() {
        cardInfo.gameObject.SetActive(false);
        cardDescription.gameObject.SetActive(false);

        returnToMenuButton.SetActive(true);
        cardsScrollSpace.SetActive(true);
        bottomPanel.SetActive(true);
        closureArea.SetActive(false);
    }

    public void BuyCard() {
        if (Coins >= cardPrice) {
            Coins -= cardPrice;
            
            var cardIndex = unlockedCards.ObtainRandomCard();
            cards[cardIndex].Unlock();
        }
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
