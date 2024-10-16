using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New AOE", menuName = "ScriptableObjects/AOE")]
public class AOE : Action
{
    public float damage;
    public float range;
    public GameObject hitEffect;

    public override void Cast(Transform target, Transform firePoint) {
        GameObject hit = Instantiate(hitEffect, new Vector3(firePoint.parent.transform.position.x, 0, firePoint.parent.transform.position.z), Quaternion.identity);
        hit.transform.localScale = new Vector3(range, .2f, range);
        Destroy(hit, .4f);
    }
}
