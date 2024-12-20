using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool alreadyDead;

    private void Awake() {
        
        currentHealth = maxHealth;
    }
        

    private void Start() {
        Invoke(nameof(SetStats), .001f);
        
        if(gameObject.CompareTag("Enemy")) {
            GameManager.instance.EnemyCount++;
        } else if(gameObject.CompareTag("Ally")) {
            GameManager.instance.AllyCount++;
        }
    }
    
    public void TakeDamage(float amount) {
        currentHealth -= amount;
        UpdateUI();

        if(currentHealth <= 0 && alreadyDead == false) {
            alreadyDead = true;
            Death();
        }
    }

    public void Heal(float amount) {
        currentHealth += amount;
        UpdateUI();

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public bool InHealingRange() {
        return currentHealth <= maxHealth * .5f;
    }

    private void Death() {
        if(gameObject.CompareTag("Enemy")) {
            GameManager.instance.EnemyCount--;

            gameObject.GetComponent<LootPool>().DropLoot();
        } else if(gameObject.CompareTag("Ally")) {
            GameManager.instance.AllyCount--;
        }
        Destroy(gameObject);
    }

    private void UpdateUI() {
        switch (gameObject.tag) {
            case "Ally":
                UIController.instance.UpdatePlayerHealthUI(currentHealth/maxHealth);
                break;
            
            case "Hero":
                UIController.instance.UpdateHeroHealthUI(currentHealth/maxHealth);
                break;
        }
    }
    
    private void SetStats() {
        maxHealth += (20 * GetComponent<BaseStats>().health);
        currentHealth = maxHealth;
    }
}
