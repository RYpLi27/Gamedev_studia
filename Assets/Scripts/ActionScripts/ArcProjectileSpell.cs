using UnityEngine;

[CreateAssetMenu(fileName = "NewConeProjectileSpell", menuName = "ScriptableObjects/Cone Projectile Spell")]
public class ConeProjectileSpell : Action {
    public float projectileSpeed;
    public float flightTime;
    public float coneAngle;
    public int projectileCount;
    public GameObject projectile;
    
    private float angleStep;
    private float startAngle;
    private Quaternion toTargetRotation;

    public override void Cast(Transform target, Transform firePoint) {
        toTargetRotation = Quaternion.LookRotation(target.position - firePoint.position);
        
        angleStep = coneAngle / ((float)projectileCount - 1);
        
        startAngle = toTargetRotation.eulerAngles.y - coneAngle / 2;
        
        for (int i = 0; i < projectileCount; i++) {
            Rigidbody rb = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            Destroy(rb.gameObject, flightTime);

            rb.GetComponent<SpellStats>().SetValues(value);
            rb.transform.rotation = Quaternion.Euler(new Vector3(0, startAngle + angleStep * i, 0));
            
            rb.AddForce(rb.transform.forward * projectileSpeed, ForceMode.Impulse);
        }
    }
}