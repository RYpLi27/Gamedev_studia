using System.Diagnostics.Tracing;
using UnityEngine;
[System.Serializable]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float sprintModifier;
    private float counter;

    [SerializeField] private Transform firePoint;
    [SerializeField] private Action[] playerActions;
    [SerializeField] private LayerMask whatIsGround;
    
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(SetStats), .001f);
    }

    private void Update()
    {
        //if (GameManager.instance.GamePaused) { return; }
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        // MOVEMENT POSTACI
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;

        rb.velocity = Input.GetKey(KeyCode.LeftShift) ? velocity * sprintModifier : velocity;
        
        // CASTOWANIE SPELLI JESZCZE NIE DOKONCZONE
        if (Input.GetMouseButtonDown(1) && counter <= 0 && playerActions[0] != null) {
            CastSpell(playerActions[0]);
            
        }
    }

    private void CastSpell(Action action)
    {
        action.Cast(GetMousePosition(), firePoint);
        counter = action.actionInterval;
    }

    private Vector3 GetMousePosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsGround)) {
            return hit.point;
        }

        return transform.position;
    }

    private void SetStats() {
        speed += (1 * GetComponent<BaseStats>().speed);
    }
}
