using UnityEngine;

public class Action : ScriptableObject
{
    public float value;
    public float castDelay;
    public string actionName;
    public float actionInterval;
    public float manaCost;
    public bool gainMana;

    public virtual void Cast(Transform target, Transform firePoint) {

    }

    public virtual void Cast(Transform target) {

    }

    public virtual void Cast(Vector3 target, Transform firePoint) {
        
    }
}
