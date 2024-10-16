using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        LevelInfo.instance.EnemyCount++;
    }

    void OnMouseDown() {
        Death();
    }

    private void Death() {
        LevelInfo.instance.EnemyCount--;
        Destroy(gameObject);
    }
}
