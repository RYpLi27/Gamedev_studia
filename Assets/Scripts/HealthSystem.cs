using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool alreadyDead;
    private GameManager gm;

    private void Awake() {
        currentHealth = maxHealth;
    }
        

    private void Start() {
        gm = GameManager.instance;

        if (CompareTag("Ally") || CompareTag("Hero")) {
            if (gm.CurrentLevel != 1) { currentHealth = CompareTag("Ally") ? gm.playerHealth : gm.heroHealth; }
            Invoke(nameof(SetStats), .001f);
        } else {
            Invoke(nameof(SetStats), .001f);
        }
        
        UpdateUI();
        
        if(gameObject.CompareTag("Enemy")) {
            gm.EnemyCount++;
        } else if(gameObject.CompareTag("Ally")) {
            gm.AllyCount++;
        }
    }

    public float GetCurrentHealth() {
        return currentHealth;
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
        //DO ZBALANSOWANIA
        maxHealth += (2 * GetComponent<BaseStats>().health);
        if(gm.CurrentLevel == 1) {currentHealth = maxHealth;}
    }
}
