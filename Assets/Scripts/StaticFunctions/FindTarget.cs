using UnityEngine;

public class FindTarget : MonoBehaviour
{
    public static Transform AtRandom(string tag) {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);

        return foundObjects.Length == 0 ? null : foundObjects[Random.Range(0, foundObjects.Length)].transform;
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

        return closestObj != null ? closestObj.transform : null;
    }
}
