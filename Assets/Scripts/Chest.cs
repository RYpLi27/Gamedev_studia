using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isOpen;

    private void Start() {
        LevelInfo.instance.ChestCount++;
    }

    public void Interact() {
        if(isOpen == false) {
            isOpen = true;
            LevelInfo.instance.ChestCount--;
            gameObject.tag = "Obstacle";
        }
    }
}
