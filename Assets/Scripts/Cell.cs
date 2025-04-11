using UnityEngine;

public class Cell : MonoBehaviour
{
    private Spaceship spaceship;

    public Spaceship PlaceSpaceship(Spaceship spaceship) {
        var placedSpaceship = Instantiate(spaceship, transform);
        this.spaceship = placedSpaceship;
        return this.spaceship;
    }

    public void RemoveSpaceship() {
        Destroy(spaceship);
        spaceship = null;
    }

    public bool IsOccupied() {
        return spaceship != null;
    }
}
