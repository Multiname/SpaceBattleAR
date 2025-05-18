using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedCards", menuName = "Unlocked Cards", order = 1)]
public class UnlockedCards : ScriptableObject 
{
    [Serializable]
    public class CardState {
        public Card card;
        public bool unlocked;
    }

    [SerializeField]
    private List<CardState> cardStates = new();

    public int playerCoins = 0;

    public void SetCardUnlocked(int index, bool unlocked) {
        cardStates[index].unlocked = unlocked;
    }

    public bool CheckCardUnlocked(int index) {
        return cardStates[index].unlocked;
    }

    public List<CardState> GetCardStates() {
        return cardStates;
    }

    public List<Card> GetUnlockedCards() {
        return cardStates.Where(x => x.unlocked).Select(x => x.card).ToList();
    }

    public int ObtainRandomCard() {
        List<Card> lockedCards = cardStates.Where(x => !x.unlocked).Select(x => x.card).ToList();
        Card cardToUnlock = lockedCards[UnityEngine.Random.Range(0, lockedCards.Count)];

        var cardToUnlockState = cardStates.First(x => x.card == cardToUnlock);
        cardToUnlockState.unlocked = true;

        return cardStates.IndexOf(cardToUnlockState);
    }
}
