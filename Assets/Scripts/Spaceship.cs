using UnityEngine;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [field: SerializeField]
    public Image CardImage { get; private set; }
    
    private Color teamColor;
    public bool Friendly { get; private set; } = true;

    private void Start() {
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
