using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    [HideInInspector]
    public int playerIndex = 0;

    private void Start() {
        meshRenderer.material.color = Color.green;
    }

    public void SetFriendliness(bool friendly) {
        if (friendly) {
             meshRenderer.material.color = Color.green;
        } else {
            meshRenderer.material.color = Color.red;
        }
    }
}
