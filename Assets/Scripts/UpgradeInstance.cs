using UnityEngine;

public class UpgradeInstance : MonoBehaviour
{
    public Upgrade template;
    public int currentLevel;

    private void Start() {
        currentLevel = PlayerPrefs.GetInt(template.upgradeName, 0);
    }
}
