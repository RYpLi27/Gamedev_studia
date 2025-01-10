using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {
    [SerializeField] private Slider hpSlider;
    
    private void Update() {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void UpdateUI(float value) {
        hpSlider.value = value;
    }
}
