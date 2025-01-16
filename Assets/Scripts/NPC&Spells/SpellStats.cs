using UnityEngine;

public class SpellStats : MonoBehaviour
{
    [SerializeField] private bool destroyOnPenetration;
    [SerializeField] private bool destroyOnCollision;
    [SerializeField] private int penetrationAmount;
    [SerializeField] private bool isHeal;
    [SerializeField] private bool fromEnemy;
    [SerializeField] private bool spawnEffectOnHit;
    [SerializeField] private GameObject effectOnHit;
    private float value;
    private Action action;

    private void OnTriggerEnter(Collider col)
    {
        if(isHeal) {
            col.GetComponent<HealthSystem>().Heal(value);
    
            DestroyCheck();
        } else if(col.CompareTag("Obstacle")) {
            DestroyCheck();
        } else {
            switch(fromEnemy, col.tag) {
                case (false, "Enemy"):
                case (true, "Hero"):
                    col.GetComponent<HealthSystem>().TakeDamage(value);

                    if (action.gainMana) {
                        ManaSystem.instance.GainMana(action.manaCost);
                    }
    
                    DestroyCheck();
                    break;
                
                case (true, "Ally"):
                    col.GetComponentInParent<HealthSystem>().TakeDamage(value);
    
                    DestroyCheck();
                    break;
            }
        }
    }

    public void SetValues(Action act, float valueBonus) {
        action = act;
        value = action.value + valueBonus;
    }

    private void DestroyCheck() {
        if (destroyOnPenetration != true) return;

        if (spawnEffectOnHit == true) Instantiate(effectOnHit, transform.position, Quaternion.identity);
        
        if (destroyOnCollision) {
            Destroy(gameObject);
        }
        
        if(penetrationAmount > 0) {
            penetrationAmount--;
        } else {
            Destroy(gameObject);
        }
    }
}
