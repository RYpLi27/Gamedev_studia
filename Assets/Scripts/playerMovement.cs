using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10.0f;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;

        rb.velocity = velocity;
    }
}
