using UnityEngine;

public class Gold : MonoBehaviour
{
    private void Start() {
        LevelInfo.instance.GoldCount++;
    }

    public void Interact() {
        print("You collected gold");
        LevelInfo.instance.GoldCount--;
        Destroy(gameObject);
    }
}
