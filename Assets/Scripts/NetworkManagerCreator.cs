using UnityEngine;

public class NetworkManagerCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject networkManager;

    private static bool initialized = false;

    private void Awake() {
        if (!initialized) {
            Instantiate(networkManager);
            initialized = true;
        }
    }
}
