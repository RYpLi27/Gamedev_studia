using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;
    private void Awake() {
        instance = this;
    }

    public int EnemyCount { get; set; }

    public int AllyCount { get; set; }

    public int ChestCount { get; set; }

    public int GoldCount { get; set; }
}
