using UnityEngine;

[CreateAssetMenu(fileName = "NewAOE", menuName = "ScriptableObjects/AOE")]
[System.Serializable]
public class AOE : Action
{
    public float range;
    public GameObject hitEffect;
    public bool frontAttack;

    public override void Cast(Transform target, Transform firePoint) {
        GameObject hit = Instantiate(hitEffect, new Vector3(firePoint.parent.transform.position.x, 0, firePoint.parent.transform.position.z), Quaternion.LookRotation(target.position - firePoint.position));
        Destroy(hit, .4f);

        if (frontAttack == true) {
            hit.GetComponentInChildren<SpellStats>().SetValues(value);
        } else {
            hit.transform.localScale = new Vector3(range * 2, .2f, range * 2);
            hit.GetComponent<SpellStats>().SetValues(value);
        }
    }
}
