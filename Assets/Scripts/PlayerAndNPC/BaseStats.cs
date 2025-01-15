using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public int health;
    [SerializeField] public int speed;
    [SerializeField] private bool overrideValues;
    
    private void Start() {
        if (overrideValues == true || transform.CompareTag("Hero")) { return; }
        // MOZE POZNIEJ COS ZMIENIC ZE SKALOWANIEM BOHATERA ALE NARAZIE NA SZTYWNO
        
        if (transform.CompareTag("Ally")) {
            damage = PlayerPrefs.GetInt("Damage", 0);
            health = PlayerPrefs.GetInt("Health", 0);
            speed = PlayerPrefs.GetInt("Speed", 0);
        } else {
            damage = GameManager.instance.CurrentLevel - 1;
            health = GameManager.instance.CurrentLevel - 1;
            speed = GameManager.instance.CurrentLevel - 1;
        }
    }
}
