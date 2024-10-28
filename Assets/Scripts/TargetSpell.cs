using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTargetSpell", menuName = "ScriptableObjects/Target Spell")]
public class TargetSpell : Action
{
    public GameObject healParticle;

    public override void Cast(Transform target)
    {
        Instantiate(healParticle, target.transform);

        target.GetComponent<HealthSystem>().Heal(value);
    }
}
