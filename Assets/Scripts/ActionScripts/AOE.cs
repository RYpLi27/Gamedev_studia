using UnityEngine;

[CreateAssetMenu(fileName = "NewAOE", menuName = "ScriptableObjects/AOE")]
[System.Serializable]
public class AOE : Action
{
    public float range;
    public GameObject hitEffect;
    public bool frontAttack;

    public override void Cast(Transform target, Transform origin) {
        GameObject hit = Instantiate(hitEffect, new Vector3(origin.parent.transform.position.x, origin.parent.transform.position.y - 1, origin.parent.transform.position.z), Quaternion.LookRotation(target.position - origin.position));
        Destroy(hit, .4f);
        
        if (frontAttack == true) {
            hit.GetComponentInChildren<SpellStats>().SetValues(value + (1 * origin.parent.GetComponent<BaseStats>().damage));
        } else {
            hit.transform.localScale = new Vector3(range * 2, .2f, range * 2);
            hit.GetComponent<SpellStats>().SetValues(value);
        }
    }
}
