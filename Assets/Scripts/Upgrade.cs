using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class Upgrade : ScriptableObject {
    public string upgradeName;
    public int cost;
    public int costPerLevel;
    // public int currentLevel;
    public int maxLevel;
}