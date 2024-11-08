using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApplyHit : MonoBehaviour
{
    [SerializeField] private bool destroyOnPenetration;
    [SerializeField] private int penetrationAmmount;
    private float value;
    private bool isFriendly;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy") && isFriendly == false) {
            col.GetComponent<HealthSystem>().TakeDamage(value);

            DestroyCheck();
        } else if(col.CompareTag("Ally") && isFriendly == true) {
            col.GetComponent<HealthSystem>().Heal(value);

            DestroyCheck();
        } else if(col.CompareTag("Obstacle")) {
            Destroy(gameObject);
        }
    }

    public void SetValues(float ammount, bool friendly) {
        value = ammount;
        isFriendly = friendly;
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
