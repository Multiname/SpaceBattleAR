using TMPro;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [field: SerializeField]
    public Card Card { get; private set; }
    [SerializeField]
    private TextMeshPro healthPointsText;
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

    private void Start() {
        HealthPoints = Card.HealthPoints;
        meshRenderer.material.color = Color.green;
        teamColor = Color.green;
    }

    public void SetFriendliness(bool friendly) {
        Friendly = friendly;
        if (friendly) {
            meshRenderer.material.color = Color.green;
            teamColor = Color.green;
        } else {
            meshRenderer.material.color = Color.red;
            teamColor = Color.red;
        }
    }

    public void Select() {
        meshRenderer.material.color = Color.blue;
    }

    public void Unselect() {
        meshRenderer.material.color = teamColor;
    }
}
