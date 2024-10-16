using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyTargetAction", menuName = "ScriptableObjects/Enemy Target")]
public class EnemyTargetAction : Action
{
    public float damage;
    public float projectileSpeed;
    public float flightTime;
    public GameObject projectile;

    public override void Cast(Transform target, Transform firePoint) {
        Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, flightTime);

        Vector3 targetPosition =  target.position - firePoint.position;
        rb.AddForce(targetPosition.normalized * projectileSpeed, ForceMode.Impulse);
    }
}
