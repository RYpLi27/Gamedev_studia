using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Action : ScriptableObject
{
    public float value;
    public float castDelay;
    public string actionName;
    public float actionInterval;

    public virtual void Cast(Transform target, Transform firePoint) {

    }

    public virtual void Cast(Transform target) {

    }
}
