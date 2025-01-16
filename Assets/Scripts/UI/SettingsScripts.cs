using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsScripts : MonoBehaviour {
    [SerializeField] private Toggle fullscreenToggle; // NIE WIDAC W EDYTORZE

    private void Start() {
        fullscreenToggle.value = Screen.fullScreen;
        GetComponent<Canvas>().enabled = false;
    }

    public void FullscreenToggle() {
        Screen.fullScreen = fullscreenToggle.value;
    }
}
