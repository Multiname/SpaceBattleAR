using TMPro;
using UnityEngine;

[RequireComponent(typeof(Skill))]
public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private Outline outline;
    [field: SerializeField]
    public Card Card { get; private set; }
    [SerializeField]
    private TextMeshProUGUI healthPointsText;
    [SerializeField]
    private GameObject actionAvailableObject;
    private bool actionAvailable = true;
    public bool ActionAvailable {
        set {
            actionAvailable = value;
            actionAvailableObject.SetActive(value);
        }
        get {
            return actionAvailable;
        }
    }
    [field: SerializeField]
    public Skill Skill { get; private set; }
    
    private Color teamColor;
    public bool Friendly { get; private set; } = true;
    [HideInInspector]
    public Cell cell;

    private int healthPoints;
    public int HealthPoints { 
        set {
            healthPoints = value;
            healthPointsText.text = value.ToString();
        }
        get {
            return healthPoints;
        }
    }

    private void Awake() {
        HealthPoints = Card.HealthPoints;
        teamColor = Color.green;
    }

    public void SetFriendliness(bool friendly) {
        Friendly = friendly;
        if (friendly) {
            outline.OutlineColor = Color.green;
            teamColor = Color.green;
        } else {
            outline.OutlineColor = Color.red;
            teamColor = Color.red;
        }
    }

    public void Select() {
        outline.OutlineColor = Color.blue;
    }

    public void Unselect() {
        outline.OutlineColor = teamColor;
    }
}
