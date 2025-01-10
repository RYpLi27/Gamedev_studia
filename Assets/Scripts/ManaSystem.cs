using UnityEngine;

public class ManaSystem : MonoBehaviour {
    public static ManaSystem instance;

    private void Awake() {
        instance = this;
    }

    [SerializeField] private float maxMana;
    private float currentMana;

    private void Start() {
        currentMana = maxMana;
    }

    public bool CheckMana(float spellCost) {
        if(spellCost > currentMana) { return false; }

        currentMana -= spellCost;
        UpdateUI();
        return true;
    }
    
    public void GainMana(float value) {
        currentMana += value;

        if (currentMana > maxMana) { currentMana = maxMana; }
        UpdateUI();
    }

    private void UpdateUI() {
        UIController.instance.UpdateManaUI(currentMana/100);
    }
}
