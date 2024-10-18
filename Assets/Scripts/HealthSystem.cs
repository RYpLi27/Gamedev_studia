using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth;
    private float currentHealth;

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

        if(currentHealth <= 0) {
            Death();
        }
    }

    public void Heal(float ammount) {
        currentHealth += ammount;

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    void Death() {
        if(gameObject.CompareTag("Enemy")) {
            LevelInfo.instance.EnemyCount--;
        } else if(gameObject.CompareTag("Ally")) {
            LevelInfo.instance.AllyCount--;
        }
        Destroy(gameObject);
    }
}
