using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateLevel();
        } else {
            Destroy(gameObject);
        }
    }

    public int EnemyCount { get; set; }

    public int AllyCount { get; set; }

    public int ChestCount { get; set; }

    public int GoldPileCount { get; set; }
    
    public int CollectedGold { get; set; }
    
    public bool GamePaused { get; set; }
    
    public int CurrentLevel { get; private set; }
    
    public Transform EntrancePoint { get; set; }
    public Transform PlayerPoint { get; set; }
    public Transform ExitPoint { get; set; }
    
    public float playerHealth { get; set; }
    public float heroHealth { get; set; }
    public float playerMana { get; set; }

    public void UpdateLevel() {
        CurrentLevel++;
    }

    public void Reset() {
        GamePaused = false;
        CurrentLevel = 1;
        CollectedGold = 0;
        GoldPileCount = 0;
        AllyCount = 0;
        EnemyCount = 0;
        ChestCount = 0;
    }
    
    public void UpdatePoints(Transform entrancePoint, Transform playerPoint, Transform exitPoint) {
        EntrancePoint = entrancePoint;
        PlayerPoint = playerPoint;
        ExitPoint = exitPoint;
        
        SpawnCharacters();
    }

    public void SpawnCharacters() {
        HeroBehaviour.instance.transform.position = EntrancePoint.position;
        HeroBehaviour.instance.exitPoint = ExitPoint;
        PlayerMovement.instance.transform.position = PlayerPoint.position;
    }
    
    public void GetPlayerHeroValues() {
        playerHealth = PlayerMovement.instance.GetComponent<HealthSystem>().GetCurrentHealth();
        playerMana = PlayerMovement.instance.GetComponent<ManaSystem>().GetManaValue();
        heroHealth = HeroBehaviour.instance.GetComponent<HealthSystem>().GetCurrentHealth();
    }
    
    public void CheckExitDistance(Transform heroPos) {
        if (Vector3.Distance(new Vector3(heroPos.position.x, 0, heroPos.position.z), new Vector3(ExitPoint.position.x, 0, ExitPoint.position.z)) < .4f) {
            UpdateLevel();
            //ZAPISANIE WARTOSCI ZDROWIA I MANY
            GetPlayerHeroValues();
            //TUTAJ ZROBIC WCZYTANIE SCENY Z UPGRADE'AMI
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
