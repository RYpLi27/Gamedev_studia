using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform targetTransform;
    float speed = 7f;
    void Update()
    {
        Vector3 odlegloscOdCelu = targetTransform.position - targetTransform.position;
        Vector3 kiedunekDoCelu = odlegloscOdCelu.normalized;
        Vector3 velocity = odlegloscOdCelu * speed;

        float dystansDoCelu = odlegloscOdCelu.magnitude;

        //if (dystansDoCelu > 1.5f)
        //{
        //}
    }
}
