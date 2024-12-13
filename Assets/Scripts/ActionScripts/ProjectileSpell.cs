using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileSpell", menuName = "ScriptableObjects/Projectile Spell")]
[System.Serializable]
public class ProjectileSpell : Action
{
    public float projectileSpeed;
    public float flightTime;
    public GameObject projectile;

    public override void Cast(Transform target, Transform firePoint) {
        Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, flightTime);

        rb.GetComponent<SpellStats>().SetValues(value);

        Vector3 targetPosition =  target.position - firePoint.position;
        rb.AddForce(targetPosition.normalized * projectileSpeed, ForceMode.Impulse);
    }
    
    public override void Cast(Vector3 target, Transform firePoint) {
        Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, flightTime);

        rb.GetComponent<SpellStats>().SetValues(value);

        Vector3 targetPosition =  new Vector3(target.x, 0, target.z) - new Vector3(firePoint.position.x, 0, firePoint.position.z);
        
        rb.AddForce(targetPosition.normalized * projectileSpeed, ForceMode.Impulse);
    }
}
