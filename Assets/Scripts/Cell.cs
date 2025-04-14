using UnityEngine;

public class Cell : MonoBehaviour
{
    [field: SerializeField]
    public int Row { get; private set; }
    [field: SerializeField]
    public int Column { get; private set; }

    private Spaceship spaceship;

    public Spaceship PlaceSpaceship(Spaceship spaceship) {
        var placedSpaceship = Instantiate(spaceship, transform);
        this.spaceship = placedSpaceship;
        placedSpaceship.cell = this;
        return this.spaceship;
    }

    public void AttachSpaceship(Spaceship spaceship) {
        Vector3 spaceshipPosition = spaceship.transform.localPosition;
        spaceship.transform.SetParent(transform);
        spaceship.transform.localPosition = spaceshipPosition;

        this.spaceship = spaceship;
        spaceship.cell = this;
    }

    public void DetachSpaceship() {
        spaceship = null;
    }

    public bool IsOccupied() {
        return spaceship != null;
    }
}
