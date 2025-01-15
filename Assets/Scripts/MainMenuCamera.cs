using Unity.VisualScripting;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void SetAnimTrigger(string triggerName) {
        anim.SetTrigger(triggerName);
    }

    public void DisableCanvas(string canvasName) {
        GameObject.Find(canvasName).GetComponent<Canvas>().enabled = false;
    }

    public void EnableCanvas(string canvasName) {
        GameObject.Find(canvasName).GetComponent<Canvas>().enabled = true;
    }
}
