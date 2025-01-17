using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HeroBehaviour : MonoBehaviour
{
    public static HeroBehaviour instance;

    private void Awake() {
        instance = this;
    }

    private bool lootFound;
    public Transform targetedLoot;
    private GameManager gameManager;
    private NavMeshAgent agent;
    private SpellCasting spells;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameManager.instance;
        spells = GetComponent<SpellCasting>();
    }

    private void Update() {
        if (gameManager.GamePaused) { return; }
        
        if(gameManager.EnemyCount == 0 && spells.enabled == true) { // BEHAVIOUR WHEN ENEMIES ARE IN THE ROOM
            spells.enabled = false;
        } 
        if(gameManager.EnemyCount == 0) { // BEHAVIOUR WHEN THERE ARE NO ENEMIES IN THE ROOM
            if(gameManager.GoldPileCount > 0) { // COLLECTING GOLD              -------------------------------------- CHANGE INTERVAL TO BE CONTROLLED BY ANIMATION STATE MACHINE -----------------------------------------
                if(lootFound == true) {
                    GoTo(targetedLoot);

                    if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetedLoot.position.x, 0, targetedLoot.position.z)) <= .1f) {
                        targetedLoot.GetComponent<Gold>().Interact();
                        lootFound = false;
                    }
                } else {
                    targetedLoot = FindTarget.ClosestTo("Gold", transform);
                    lootFound = true;
                }
            } else if(gameManager.ChestCount > 0) { // LOOTING CHESTS                 -------------------------------------- CHANGE INTERVAL TO BE CONTROLLED BY ANIMATION STATE MACHINE -----------------------------------------
                if(lootFound == true) {
                    GoTo(targetedLoot);

                    if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetedLoot.position.x, 0, targetedLoot.position.z)) <= .1f) {
                        targetedLoot.GetComponent<Chest>().Interact();
                        lootFound = false;
                    }
                } else {
                    targetedLoot = FindTarget.ClosestTo("Chest", transform);
                    if(targetedLoot != null) {
                        lootFound = true;
                    }
                }
            } else { // PROCEED TO NEXT ROOM
                // go to next room and then wait for player
                agent.SetDestination(ExitPoint.instance.transform.position);
            }
        }

#if(UNITY_EDITOR)
    if(Input.GetKeyDown(KeyCode.R))
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
    }

    private void GoTo(Transform target) {
        agent.SetDestination(target.position);
    }
}
