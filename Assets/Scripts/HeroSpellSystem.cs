using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HeroSpellSystem : MonoBehaviour
{
    private Transform targetedEnemy, targetedAlly, actionTarget;
    private bool approachEnemy, readyToCast, tryRecast, canMove, targetInSight, lootFound;
    private Rigidbody rb;
    private float actionIntervalTimer, approachEnemyRange;
    private Action currentAction;
    private LevelInfo levelInfo;


    [Header("Stats")]
    [SerializeField] private float spaceEnemyRange;
    [SerializeField] private float sightLength;

    [Header("Transforms etc.")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask whatToSee;

    [Header("Actions")]
    [SerializeField] private Action[] actionListEnemy;
    [SerializeField] private Action[] actionListIdle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        readyToCast = true;
        agent = GetComponent<NavMeshAgent>();
        currentAction = actionListEnemy[Random.Range(0, actionListEnemy.Length)];
        levelInfo = LevelInfo.instance;
    }

    void Update()
    {
        if(levelInfo.EnemyCount > 0) { // BEHAVIOUR WHEN ENEMIES ARE IN THE ROOM
            // SEARCHING FOR A TARGET
            if(targetedEnemy == null) {
                targetedEnemy = GameObject.FindWithTag("Enemy").transform;
                if(actionTarget == null) {
                    actionTarget = targetedEnemy;
                }
            }

            if(targetedAlly == null && levelInfo.AllyCount > 0) {
                targetedAlly = GameObject.FindWithTag("Ally").transform;
            }

            // LINE OF SIGHT
            if(actionTarget != null) {
                if(Physics.Raycast(transform.position, actionTarget.position - transform.position, out RaycastHit sightHit, sightLength, whatToSee, QueryTriggerInteraction.Ignore)) {
                    if(sightHit.transform == actionTarget.transform) {
                        targetInSight = true;

                        if(currentAction is AOE == false) {
                            approachEnemy = false;

                            if(tryRecast == true) {
                                tryRecast = false;
                                MakeAction();
                            }
                        }
                    } else {
                        targetInSight = false;
                        approachEnemy = true;
                    }
                } 
            }

            // CHOOSING NEXT ACTION IF ABLE
            if(actionIntervalTimer > 0) {
                actionIntervalTimer -= Time.deltaTime;
            } else if(readyToCast == true) {
                canMove = true;
                MakeAction();
            }

            // HERO MOVEMENT
            if(canMove == true) {
                if(approachEnemy == true) {
                    ApproachEnemy();
                } else{
                    SpaceEnemy();
                } 
            } else {
                agent.SetDestination(transform.position);
            }
        } else { // BEHAVIOUR WHEN THERE ARE NO ENEMIES IN THE ROOM
            if(levelInfo.GoldCount > 0) { // COLLECTING GOLD              -------------------------------------- CHANGE INTERVAL TO BE CONTROLLED BY ANIMATION STATE MACHINE -----------------------------------------
                if(lootFound == true) {
                    agent.SetDestination(actionTarget.position);

                    if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(actionTarget.position.x, 0, actionTarget.position.z)) <= .1f) {
                        actionTarget.GetComponent<Gold>().Interact();
                        lootFound = false;
                    }
                } else {
                    actionTarget = actionTarget = GameObject.FindWithTag("Gold").transform;
                    lootFound = true;
                }
            } else if(levelInfo.ChestCount > 0) { // LOOTING CHESTS                 -------------------------------------- CHANGE INTERVAL TO BE CONTROLLED BY ANIMATION STATE MACHINE -----------------------------------------
                if(lootFound == true) {
                    agent.SetDestination(actionTarget.position);

                    if(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(actionTarget.position.x, 0, actionTarget.position.z)) <= .1f) {
                        actionTarget.GetComponent<Chest>().Interact();
                        lootFound = false;
                    }
                } else {
                    actionTarget = actionTarget = GameObject.FindWithTag("Chest").transform;
                    lootFound = true;
                }
            } else {
                // go to next room and then wait for player
                agent.SetDestination(transform.position);
                print("next room");
            }
        }

#if(UNITY_EDITOR)
    if(Input.GetKey(KeyCode.F)) {
        Time.timeScale = .1f;
    } else {
        Time.timeScale = 1;
    }

    if(Input.GetKeyDown(KeyCode.R))
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
    }

    void MakeAction() {
        readyToCast = false;
        if(currentAction is AOE aoe) {
            actionTarget = targetedEnemy;

            if(Vector3.Distance(transform.position, targetedEnemy.position) > aoe.range/2 - 1) {
                approachEnemy = true;
                approachEnemyRange = aoe.range/2 - 1;
            } else {
                approachEnemy = false;
                canMove = false;

                Invoke("CastSpell", currentAction.castDelay);
            }
        } else {
            actionTarget = currentAction is ProjectileSpell ? targetedEnemy : targetedAlly;
            
            if(targetInSight == true) {
                approachEnemy = false;
                Invoke("CastSpell", currentAction.castDelay);
            } else {
                tryRecast = true;
            }
        }
    }

    void CastSpell() {
        if(currentAction is TargetSpell) {
            currentAction.Cast(actionTarget);
            actionTarget = null;
        } else {
            currentAction.Cast(actionTarget, firePoint);
        }

        ResetInterval(currentAction.actionInterval);
        RollAction();
    }

    public void ResetInterval(float time) {
        actionIntervalTimer = time;
        readyToCast = true;
    }

    void ApproachEnemy() {
        if(Vector3.Distance(transform.position, targetedEnemy.position) > approachEnemyRange && actionTarget != null) {
            agent.SetDestination(actionTarget.position);
        } else {
            MakeAction();
        }
    }

    void SpaceEnemy() {
        if(actionTarget != null) {
            if(Vector3.Distance(transform.position, actionTarget.position) < spaceEnemyRange) {
                Vector3 target = (transform.position - actionTarget.position).normalized;
                agent.SetDestination(transform.position + target);
            } else if(Vector3.Distance(transform.position, actionTarget.position) > spaceEnemyRange + 2) {
                agent.SetDestination(actionTarget.position);
            } else {
                agent.SetDestination(transform.position);
            }
        }
    }

    void GoTo(Transform target) {
        agent.SetDestination(target.position);
    }

    void RollAction() {
        currentAction = actionListEnemy[Random.Range(0, actionListEnemy.Length)];

        if(currentAction is TargetSpell ts) {
            actionTarget = targetedAlly;
        } else {
            actionTarget = targetedEnemy;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        if(actionTarget != null)
        Gizmos.DrawRay(transform.position, actionTarget.position - transform.position);
    }
}
