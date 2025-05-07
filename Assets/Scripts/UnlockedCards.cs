using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedCards", menuName = "Unlocked Cards", order = 1)]
public class UnlockedCards : ScriptableObject
{
    [System.Serializable]
    public class CardState {
        public Card card;
        public bool unlocked;
    }

    [SerializeField]
    private List<CardState> cardStates = new();

    public int PlayerCoins = 0;

    public List<CardState> GetCardStates() {
        return cardStates;
    }

    public List<Card> GetUnlockedCards() {
        return cardStates.Where( x => x.unlocked ).Select( x => x.card ).ToList();
    }

    public int ObtainRandomCard() {
        List<Card> lockedCards = cardStates.Where(x => !x.unlocked).Select(x => x.card).ToList();
        Card cardToUnlock = lockedCards[Random.Range(0, lockedCards.Count)];

        var cardToUnlockState = cardStates.First(x => x.card == cardToUnlock);
        cardToUnlockState.unlocked = true;
        
        return cardStates.IndexOf(cardToUnlockState);
    }
}
