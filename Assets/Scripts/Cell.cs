using UnityEngine;

public class Cell : MonoBehaviour
{
    private GameObject spaceship;

    public GameObject PlaceSpaceship(GameObject spaceship) {
        var placedSpaceship = Instantiate(spaceship, transform);
        this.spaceship = placedSpaceship;
        return placedSpaceship;
    }

    public void RemoveSpaceship() {
        Destroy(spaceship);
        spaceship = null;
    }

    public bool IsOccupied() {
        return spaceship != null;
    }
}
