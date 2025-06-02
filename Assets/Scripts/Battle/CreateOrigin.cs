using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateOrigin : MonoBehaviour {
    private ARTrackedImageManager trackedImagesManager;
    [SerializeField]
    private GameObject originPrefab;
    private bool instantiated = false;
    [SerializeField]
    private GameManager gameManager;

    void Awake() {
        trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable() {
        trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable() {
        trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        if (eventArgs.updated.Count > 0 && eventArgs.updated[0].trackingState == TrackingState.Tracking && !instantiated) {
            instantiated = true;
            var origin = Instantiate(originPrefab, eventArgs.updated[0].transform);
            gameManager.CreateBattlefield(origin);
        }
    }
}
