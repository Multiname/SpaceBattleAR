using UnityEngine;

public class HealthPointsText : MonoBehaviour
{
    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        transform.LookAt(mainCamera.transform);
    }
}
