using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sprintModifier;

    [SerializeField] private Action[] playerActions;
    [SerializeField] private LayerMask whatIsGround;
    
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.instance.GamePaused) { return; }
        
        // MOVEMENT POSTACI
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;

        rb.velocity = Input.GetKey(KeyCode.LeftShift) ? velocity * sprintModifier : velocity;
        
        // CASTOWANIE SPELLI JESZCZE NIE DOKONCZONE
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerActions[0] != null) {
            CastSpell(playerActions[0]);
        }
    }

    private void CastSpell(Action action) {
        
        action.Cast(GetMousePosition(), transform);
    }

    private Vector3 GetMousePosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsGround)) {
            return hit.point;
        }

        return transform.position;
    }
}
