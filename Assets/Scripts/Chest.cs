using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isOpen;

    void Start() {
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
