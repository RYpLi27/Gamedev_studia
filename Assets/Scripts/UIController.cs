using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;
    
    private void Awake() {
        instance = this;
    }
    
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider heroHealthBar;
    [SerializeField] private Slider manaBar;

    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject playerCanvas;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseCanvas.SetActive(pauseCanvas.activeSelf == false);
            playerCanvas.SetActive(playerCanvas.activeSelf == false);

            GameManager.instance.GamePaused = pauseCanvas.activeSelf;

            Time.timeScale = GameManager.instance.GamePaused ? 0 : 1;
        }
    }

    public void UpdateHeroHealthUI(float value) {
        heroHealthBar.value = value;
    }
    
    public void UpdatePlayerHealthUI(float value) {
        playerHealthBar.value = value;
    }

    public void UpdateManaUI(float value) {
        manaBar.value = value;
    }
}
