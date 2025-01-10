using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    [SerializeField] private float speed;
    [SerializeField] private float sprintModifier;

    [SerializeField] private Transform firePoint;
    [SerializeField] private List<Action> playerActions;
    private int selectedActionIndex;
    private List<float> cdCounters = new List<float> {0, 0, 0, 0, 0};
    private List<float> cds = new List<float> {0, 0, 0, 0, 0};
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody rb;
    private ManaSystem mana;
    private UIController ui;

    private readonly List<string> spellSlots = new List<string> { "1", "2", "3", "4", "5" };

    private void Start() {
        rb = GetComponent<Rigidbody>();
        mana = GetComponent<ManaSystem>();
        ui = UIController.instance;
        Invoke(nameof(SetStats), .001f);
        selectedActionIndex = 0;

        for (int i = 0; i < playerActions.Count; i++) {
            print(playerActions[i]);
            cds[i] = playerActions[i].actionInterval;
        }
    }

    private void Update()
    {
        if (GameManager.instance.GamePaused) { return; }
        for (int i = 0; i < cdCounters.Count; i++) {
            if (cdCounters[i] > 0)
            {
                cdCounters[i] -= Time.deltaTime;
            }
        }
        UpdateUI();
        
        // MOVEMENT POSTACI
        Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;

        rb.velocity = Input.GetKey(KeyCode.LeftShift) ? velocity * sprintModifier : velocity;
        
        // WYBIERANIE SPELLA
        // selectedAction = Input.inputString switch {
        //     "1" => playerActions[0],
        //     "2" => playerActions[1],
        //     _ => selectedAction
        // };

        if (spellSlots.Contains(Input.inputString)) {
            if (playerActions.Count < int.Parse(Input.inputString)) { return; }
            
            selectedActionIndex = int.Parse(Input.inputString) - 1;
        }

        // CASTOWANIE SPELLI JESZCZE NIE DOKONCZONE
        if (Input.GetMouseButtonDown(0) && cdCounters[selectedActionIndex] <= 0) {
            if (!playerActions[selectedActionIndex].gainMana) {
                if (!mana.CheckMana(playerActions[selectedActionIndex].manaCost)) { return; }
            }
            
            CastSpell(playerActions[selectedActionIndex]);
        }
    }

    private void CastSpell(Action action) {
        action.Cast(GetMousePosition(), firePoint);
        
        cdCounters[selectedActionIndex] = action.actionInterval;
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

    private void UpdateUI() {
        ui.UpdateSpellsUI(cdCounters, cds);
    }
}
