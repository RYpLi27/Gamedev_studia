using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : MonoBehaviour
{
    public static Transform AtRandom(string tag) {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);

        if(foundObjects.Length == 0) {
            return null;
        } else {
            return foundObjects[Random.Range(0, foundObjects.Length)].transform;
        }
    }

    public static Transform ClosestTo(string tag, Transform origin) {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);

        float closestDistance = Mathf.Infinity;
        GameObject closestObj = null;

        foreach(GameObject obj in foundObjects) {
            float distance = Vector2.Distance(origin.position, obj.transform.position);

            if(distance < closestDistance) {
                closestDistance = distance;
                closestObj = obj;
            }
        }

        return closestObj.transform;
    }
}
