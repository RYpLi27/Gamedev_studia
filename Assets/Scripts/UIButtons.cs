using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIButtons : MonoBehaviour
{
    public void LoadSceneButton(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void EnableCanvasButton(GameObject toEnable) {
        toEnable.SetActive(true);
    }
    
    public void DisableCanvasButton(GameObject toDisable) {
        toDisable.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void SaveReset() {
        PlayerPrefs.DeleteAll();
    }

    public void BuyUpgrade(string upgradeName) {
        UpgradeInstance upgrade = UpgradeManager.instance.upgrades.FirstOrDefault(item => item.template.upgradeName == upgradeName);
        int upgradeCost = upgrade.template.cost + (upgrade.template.costPerLevel * upgrade.currentLevel);
        int currentGold = GetGold();
        
        print(upgradeCost);
        print(currentGold);
        
        if (currentGold >= upgradeCost) {
            if (upgrade.template.maxLevel > upgrade.currentLevel) {
                upgrade.currentLevel++;
                Debug.Log(currentGold - upgradeCost);
                PlayerPrefs.SetInt("Gold", currentGold - upgradeCost);
                PlayerPrefs.SetInt(upgrade.template.upgradeName, upgrade.currentLevel);
            }
        }
    }

    private int GetGold() {
        return PlayerPrefs.GetInt("Gold", 0);
    }

    public void SetGold() {
        PlayerPrefs.SetInt("Gold", GetGold() + GameManager.instance.CollectedGold);
    }
    
    //FOR TESTING PURPOSES ONLY
    public void CheckGold() {
        Debug.Log(GetGold());
    }

    public void GoldForTesting() {
        PlayerPrefs.SetInt("Gold", 100000);
    }
}
