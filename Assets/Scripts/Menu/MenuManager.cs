using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private UnlockedCards unlockedCards;

    private void Start() {
        coinsText.SetText(unlockedCards.playerCoins.ToString());
    }

    public void StartGame() {
        SceneManager.LoadScene("Lobby");
    }

    public void OpenCollection() {
        SceneManager.LoadScene("Collection");
    }
}
