using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isOpen;

    private void Start() {
        GameManager.instance.ChestCount++;
    }

    public void Interact() {
        if(isOpen == false) {
            isOpen = true;
            GameManager.instance.ChestCount--;
            gameObject.tag = "Obstacle";
        }
    }
}
