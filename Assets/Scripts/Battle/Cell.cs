using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum OccupationState {
        UNOCCUPIED,
        FRIENDLY,
        HOSTILE
    }

    [field: SerializeField]
    public int Row { get; private set; }
    [field: SerializeField]
    public int Column { get; private set; }

    private Spaceship spaceship;

    public Spaceship PlaceSpaceship(Spaceship spaceship, int playerIndex, bool hostile) {
        var placedSpaceship = Instantiate(spaceship, transform);
        this.spaceship = placedSpaceship;
        placedSpaceship.cell = this;
        if (playerIndex != 0) {
            placedSpaceship.transform.Rotate(new(0, 180, 0));
        }
        if (hostile) {
            placedSpaceship.SetFriendliness(false);
        }
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

    public OccupationState CheckOccupationState() {
        if (spaceship == null) {
            return OccupationState.UNOCCUPIED;
        } else if (spaceship.Friendly) {
            return OccupationState.FRIENDLY;
        }
        return OccupationState.HOSTILE;
    }
}
