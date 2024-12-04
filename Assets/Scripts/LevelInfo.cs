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

    private int allyCount;
    public int AllyCount {
        get { return allyCount; }
        set { allyCount = value; }
    }

    private int chestCount;
    public int ChestCount {
        get { return chestCount; }
        set { chestCount = value; }
    }

    private int goldCount;
    public int GoldCount {
        get { return goldCount; }
        set { goldCount = value; }
    }
}
