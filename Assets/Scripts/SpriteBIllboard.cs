using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBIllboard : MonoBehaviour
{
    [SerializeField] bool freezeXZ = true;
    void LateUpdate()
    {
        if (freezeXZ)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
