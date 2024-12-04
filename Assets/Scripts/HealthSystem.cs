using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool alreadyDead;

    private void Start() {
        currentHealth = maxHealth;

        if(gameObject.CompareTag("Enemy")) {
            LevelInfo.instance.EnemyCount++;
        } else if(gameObject.CompareTag("Ally")) {
            LevelInfo.instance.AllyCount++;
        }
    }

    public void TakeDamage(float amount) {
        currentHealth -= amount;

        if(currentHealth <= 0 && alreadyDead == false) {
            alreadyDead = true;
            Death();
        }
    }

    public void Heal(float amount) {
        currentHealth += amount;

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public bool InHealingRange() {
        return currentHealth <= maxHealth * .5f;
    }

    private void Death() {
        if(gameObject.CompareTag("Enemy")) {
            LevelInfo.instance.EnemyCount--;

            gameObject.GetComponent<LootPool>().DropLoot();
        } else if(gameObject.CompareTag("Ally")) {
            LevelInfo.instance.AllyCount--;
        }
        Destroy(gameObject);
    }
}
