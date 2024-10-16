using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;
    private void Awake() {
        instance = this;
    }

    private int enemyCount;
    public int EnemyCount {
        get { return enemyCount; }
        set { enemyCount = value; }
    }
}
