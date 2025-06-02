using UnityEngine;

public class MenuSpaceshipDriver : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody rb;

    private float startCoordinate;

    private void Start() {
        startCoordinate = transform.position.x;
        rb.velocity = transform.TransformDirection(Vector3.forward) * speed;
    }

    private void FixedUpdate() {
        if (transform.position.x > -startCoordinate) {
            Destroy(gameObject);
        }
    }
}
