using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth;

    [Header("Only for enemy")]
    [SerializeField] private float goldDropRate;
    [SerializeField] private GameObject[] lootPool;
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

            if(Random.Range(0, 100) <= goldDropRate) {
                Instantiate(lootPool[0], new Vector3(transform.position.x, 0 , transform.position.z), Quaternion.identity);
            }
        } else if(gameObject.CompareTag("Ally")) {
            LevelInfo.instance.AllyCount--;
        }
        Destroy(gameObject);
    }
}
