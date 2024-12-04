using UnityEngine;
using UnityEngine.SceneManagement;

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
}
