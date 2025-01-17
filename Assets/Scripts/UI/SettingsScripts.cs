using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScripts : MonoBehaviour {
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionChange;

    public void SetValues() {
        fullscreenToggle.isOn = Screen.fullScreen;

        print(Screen.height);
        
        resolutionChange.value = Screen.height switch {
            1440 => 0,
            1080 => 1,
            720 => 2,
            480 => 3,
            _ => resolutionChange.value
        };
    }

    public void FullscreenToggle() {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void ResolutionChange() {
        switch (resolutionChange.value) {
            case 0:
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
            
            case 1:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            
            case 3:
                Screen.SetResolution(640, 480, Screen.fullScreen);
                break;
        }
    }
}
