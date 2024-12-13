using UnityEngine;

[CreateAssetMenu(fileName = "NewTargetSpell", menuName = "ScriptableObjects/Target Spell")]
[System.Serializable]
public class TargetSpell : Action
{
    public GameObject healParticle;

    public override void Cast(Transform target)
    {
        Instantiate(healParticle, target.transform);

        target.GetComponent<HealthSystem>().Heal(value);
    }
}
