using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    void Start() {
        LevelInfo.instance.GoldCount++;
    }

    public void Interact() {
        print("You collected gold");
        LevelInfo.instance.GoldCount--;
        Destroy(gameObject);
    }
}
