using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;
    
    private void Awake() {
        instance = this;
    }
    
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider heroHealthBar;

    public void UpdateHeroHealthUI(float value) {
        heroHealthBar.value = value;
    }
    
    public void UpdatePlayerHealthUI(float value) {
        playerHealthBar.value = value;
    }
}
