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
        coinsText.SetText(unlockedCards.PlayerCoins.ToString());
    }

    public void StartGame() {
        SceneManager.LoadScene("Battle");
    }

    public void OpenCollection() {
        SceneManager.LoadScene("Collection");
    }
}
