using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CreateOrigin : MonoBehaviour
{
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

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (eventArgs.added.Count > 0 && !instantiated) {
            instantiated = true;
            var origin = Instantiate(originPrefab, eventArgs.added[0].transform);
            gameManager.CreateBattlefield(origin);
        }
    }
}
