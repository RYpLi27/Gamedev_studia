using UnityEngine;

[CreateAssetMenu(fileName = "NewAOE", menuName = "ScriptableObjects/AOE")]
[System.Serializable]
public class AOE : Action
{
    public float range;
    public float existTime;
    public GameObject hitEffect;
    public bool frontAttack;

    public override void Cast(Transform target, Transform origin) {
        GameObject hit = Instantiate(hitEffect, origin.parent);
        Destroy(hit, existTime);
        
        if (frontAttack == true) {
            hit.transform.rotation = Quaternion.LookRotation(target.position - origin.position);
        } else {
            hit.transform.localScale = new Vector3(range * 2, .2f, range * 2);
        }

        hit.GetComponentInChildren<SpellStats>().SetValues(this, 1 * origin.parent.GetComponent<BaseStats>().damage);
    }
    
    public override void Cast(Vector3 target, Transform origin) {
        GameObject hit = Instantiate(hitEffect, origin.parent);
        Destroy(hit, existTime);
        
        if (frontAttack == true) {
            hit.transform.rotation = Quaternion.LookRotation(target - origin.position);
        } else {
            hit.transform.localScale = new Vector3(range * 2, .2f, range * 2);
        }

        hit.GetComponentInChildren<SpellStats>().SetValues(this, 1 * origin.parent.GetComponent<BaseStats>().damage);
    }
}
