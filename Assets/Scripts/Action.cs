using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Action : ScriptableObject
{
    public string actionName;
    public float actionInterval;

    public virtual void Cast(Transform target, Transform firePoint) {

    }
}
