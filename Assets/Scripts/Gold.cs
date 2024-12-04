using UnityEngine;

public class Gold : MonoBehaviour {
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;

    private int value;
    
    
    private void Start() {
        GameManager.instance.GoldPileCount++;

        value = Random.Range(minValue, maxValue + 1);
    }

    public void Interact() {
        GameManager.instance.GoldPileCount--;

        GameManager.instance.CollectedGold += value;
        
        Destroy(gameObject);
    }
}