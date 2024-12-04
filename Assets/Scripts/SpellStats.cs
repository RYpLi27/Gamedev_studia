using UnityEngine;

public class SpellStats : MonoBehaviour
{
    [SerializeField] private bool destroyOnPenetration;
    [SerializeField] private int penetrationAmount;
    [SerializeField] private bool isHeal;
    [SerializeField] private bool fromEnemy;
    private float value;

    private void OnTriggerEnter(Collider col)
    {
        if(isHeal) {
            col.GetComponent<HealthSystem>().Heal(value);

            DestroyCheck();
        } else if(col.CompareTag("Obstacle")) {
            Destroy(gameObject);
        } else {
            switch(fromEnemy, col.tag) {
                case (false, "Enemy"):
                case (true, "Hero"):
                    col.GetComponent<HealthSystem>().TakeDamage(value);

                    DestroyCheck();
                    break;
                
                case (true, "Ally"):
                    col.GetComponentInParent<HealthSystem>().TakeDamage(value);

                    DestroyCheck();
                    break;
            }
        }
    }

    public void SetValues(float amount) {
        value = amount;
    }

    private void DestroyCheck() {
        if (destroyOnPenetration != true) return;
        if(penetrationAmount > 0) {
            penetrationAmount--;
        } else {
            Destroy(gameObject);
        }
    }
}
