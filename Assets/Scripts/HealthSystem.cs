using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool alreadyDead;

    void Start() {
        currentHealth = maxHealth;

        if(gameObject.CompareTag("Enemy")) {
            LevelInfo.instance.EnemyCount++;
        } else if(gameObject.CompareTag("Ally")) {
            LevelInfo.instance.AllyCount++;
        }
    }

    public void TakeDamage(float ammount) {
        currentHealth -= ammount;

        if(currentHealth <= 0 && alreadyDead == false) {
            alreadyDead = true;
            Death();
        }
    }

    public void Heal(float ammount) {
        currentHealth += ammount;

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public bool InHealingRange() {
        if(currentHealth <= maxHealth * .5f) {
            return true;
        } else {
            return false;
        }
    }

    void Death() {
        if(gameObject.CompareTag("Enemy")) {
            LevelInfo.instance.EnemyCount--;

            gameObject.GetComponent<LootPool>().DropLoot();
        } else if(gameObject.CompareTag("Ally")) {
            LevelInfo.instance.AllyCount--;
        }
        Destroy(gameObject);
    }
}
