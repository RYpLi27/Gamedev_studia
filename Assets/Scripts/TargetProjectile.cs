using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "New TargetProjectile", menuName = "ScriptableObjects/Target Projectile")]
public class TargetProjectile : Action
{
    public bool isFriendly;
    public float projectileSpeed;
    public float flightTime;
    public GameObject projectile;

    public override void Cast(Transform target, Transform firePoint) {
        Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, flightTime);

        rb.GetComponent<ApplyHit>().SetValues(value, isFriendly);

        Vector3 targetPosition =  target.position - firePoint.position;
        rb.AddForce(targetPosition.normalized * projectileSpeed, ForceMode.Impulse);
    }
}
