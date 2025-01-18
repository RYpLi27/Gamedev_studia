using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.VisualScripting.Icons;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.Rendering.VirtualTexturing;

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
        
        if (GameManager.instance.EnemyCount > 0)
        {
            transform.LookAt(spellCasting.actionTarget);
        }
        else if (GameManager.instance.GoldPileCount > 0)
        {
            transform.LookAt(HeroBehaviour.instance.targetedLoot);
        }
        else
        {
            transform.LookAt(GameManager.instance.ExitPoint);
        }

    }
}