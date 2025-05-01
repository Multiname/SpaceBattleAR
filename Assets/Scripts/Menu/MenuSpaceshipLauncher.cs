using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpaceshipLauncher : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spaceships = new();
    [SerializeField]
    private GameObject spaceshipDriver;
    [SerializeField]
    private float cooldown = 5.0f;

    private int currentIndex = 0;

    private void Start() {
        StartCoroutine(LaunchShip());
    }

    private IEnumerator LaunchShip() {
        var driver = Instantiate(spaceshipDriver);
        var spaceship = Instantiate(spaceships[currentIndex]);

        spaceship.transform.SetParent(driver.transform);
        spaceship.transform.localPosition = Vector3.zero;
        spaceship.transform.localRotation = Quaternion.Euler(Vector3.zero);

        ++currentIndex;
        if (currentIndex >= spaceships.Count) {
            currentIndex = 0;
        }

        yield return new WaitForSeconds(cooldown);
        StartCoroutine(LaunchShip());
    }
}
