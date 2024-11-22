using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellStats : MonoBehaviour
{
    [SerializeField] private bool destroyOnPenetration;
    [SerializeField] private int penetrationAmmount;
    [SerializeField] private bool isHeal;
    [SerializeField] private bool fromEnemy;
    private float value;

    void OnTriggerEnter(Collider col)
    {
        if(isHeal == true) {
            col.GetComponent<HealthSystem>().Heal(value);

            DestroyCheck();
        } else if(col.CompareTag("Obstacle")) {
            Destroy(gameObject);
        } else {
            switch(fromEnemy, col.tag) {
                case (false, "Enemy"):
                    col.GetComponent<HealthSystem>().TakeDamage(value);

                    DestroyCheck();
                break;

                case (true, "Ally"):
                    col.GetComponent<HealthSystem>().TakeDamage(value);

                    DestroyCheck();
                break;

                case (true, "Hero"):
                    col.GetComponent<HealthSystem>().TakeDamage(value);

                    DestroyCheck();
                break;

                default:
                break;
            }
        }
    }

    public void SetValues(float ammount) {
        value = ammount;
    }

    void DestroyCheck() {
        if(destroyOnPenetration == true) {
            if(penetrationAmmount > 0) {
                penetrationAmmount--;
            } else {
                Destroy(gameObject);
            }
        }
    }
}
