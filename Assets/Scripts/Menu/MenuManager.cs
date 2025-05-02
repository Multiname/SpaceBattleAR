using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinsText;

    private void Start() {
        int coins = PlayerPrefs.GetInt("PlayerCoins", 0);
        coinsText.SetText(coins.ToString());
    }

    public void StartGame() {
        SceneManager.LoadScene("Battle");
    }
}
