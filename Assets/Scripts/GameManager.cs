using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        instance = this;
    }

    public int EnemyCount { get; set; }

    public int AllyCount { get; set; }

    public int ChestCount { get; set; }

    public int GoldPileCount { get; set; }
    
    public int CollectedGold { get; set; }
    
    public bool GamePaused { get; set; }
}
