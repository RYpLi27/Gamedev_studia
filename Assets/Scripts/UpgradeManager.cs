using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
    public static UpgradeManager instance;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    [SerializeField] public List<UpgradeInstance> upgrades;

    private void Start() {
        foreach(UpgradeInstance upgrade in upgrades) {
            upgrade.currentLevel = PlayerPrefs.GetInt(upgrade.template.upgradeName, 0);
        }
    }
}