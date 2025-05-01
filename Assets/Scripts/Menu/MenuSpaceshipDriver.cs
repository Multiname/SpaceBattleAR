using UnityEngine;

public class MenuSpaceshipDriver : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float startCoordinate;

    private void Start() {
        startCoordinate = transform.position.x;
    }

    private void FixedUpdate() {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        if (transform.position.x > -startCoordinate) {
            Destroy(gameObject);
        }
    }
}
