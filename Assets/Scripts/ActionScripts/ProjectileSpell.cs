using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileSpell", menuName = "ScriptableObjects/Projectile Spell")]
[System.Serializable]
public class ProjectileSpell : Action
{
    public float projectileSpeed;
    public float flightTime;
    public GameObject projectile;

    public override void Cast(Transform target, Transform origin) {
        Rigidbody rb = Instantiate(projectile, origin.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, flightTime);

        rb.GetComponent<SpellStats>().SetValues(this, 1 * origin.parent.GetComponent<BaseStats>().damage);

        Vector3 targetPosition =  target.position - origin.position;
        rb.AddForce(targetPosition.normalized * projectileSpeed, ForceMode.Impulse);
    }
    
    public override void Cast(Vector3 target, Transform origin) {
        Rigidbody rb = Instantiate(projectile, origin.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, flightTime);

        rb.GetComponent<SpellStats>().SetValues(this, 1 * origin.parent.GetComponent<BaseStats>().damage);

        Vector3 targetPosition =  new Vector3(target.x, 0, target.z) - new Vector3(origin.position.x, 0, origin.position.z);
        
        rb.AddForce(targetPosition.normalized * projectileSpeed, ForceMode.Impulse);
    }
}
