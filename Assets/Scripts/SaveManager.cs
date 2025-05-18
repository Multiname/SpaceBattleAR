using System;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    [SerializeField]
    private UnlockedCards unlockedCards;

    private static SaveManager instance = null;

    private void Awake() {
        if (instance == null) {
            unlockedCards.playerCoins = PlayerPrefs.GetInt("PlayerCoins");

            int AdvancedTieFighterUnlocked = PlayerPrefs.GetInt("AdvancedTieFighter");

            unlockedCards.SetCardUnlocked(1, AdvancedTieFighterUnlocked == 1);

            DontDestroyOnLoad(gameObject);
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        if (this == instance) {
            PlayerPrefs.SetInt("PlayerCoins", unlockedCards.playerCoins);

            PlayerPrefs.SetInt("AdvancedTieFighter", Convert.ToInt32(unlockedCards.CheckCardUnlocked(1)));

            PlayerPrefs.Save();
        }
    }
}
