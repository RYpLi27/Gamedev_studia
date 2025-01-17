using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    //[SerializeField] private Transform targetTransform;
    [SerializeField] private SpellCasting spellCasting;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        //_transform.LookAt(targetTransform);
        //spellCasting.actionTarget.
        transform.LookAt(spellCasting.actionTarget);
        if (GameManager.instance.EnemyCount < 1)
        {
            transform.LookAt(HeroBehaviour.instance.targetedLoot);
        }
        else
        {
            transform.LookAt(spellCasting.actionTarget);
        }

    }

}
