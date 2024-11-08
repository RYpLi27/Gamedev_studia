using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class HeroSpellSystem : MonoBehaviour
{
    private Transform targetedEnemy, targetedAlly, actionTarget;
    private bool approachEnemy, readyToCast, canMove;
    private Rigidbody rb;
    private float actionIntervalTimer, approachEnemyRange;
    private Action currentAction;

    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float spaceEnemyRange;

    [Header("Transforms etc.")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private NavMeshAgent agent;

    [Header("Actions")]
    [SerializeField] private Action[] actionListEnemy;
    [SerializeField] private Action[] actionListIdle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        readyToCast = true;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(LevelInfo.instance.EnemyCount > 0) { // BEHAVIOUR WHEN ENEMIES ARE IN THE ROOM
            // SEARCHING FOR A TARGET
            if(targetedEnemy == null) {
                targetedEnemy = GameObject.FindWithTag("Enemy").transform;
                if(actionTarget == null) {
                    actionTarget = targetedEnemy;
                }
            }

            if(targetedAlly == null && LevelInfo.instance.AllyCount > 0) {
                targetedAlly = GameObject.FindWithTag("Ally").transform;
            }

            // CHOOSING NEXT ACTION IF ABLE
            if(actionIntervalTimer > 0) {
                actionIntervalTimer -= Time.deltaTime;
            } else if(readyToCast == true) {
                canMove = true;
                currentAction = actionListEnemy[Random.Range(0, actionListEnemy.Length)];
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

        }
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
                ResetInterval(currentAction.actionInterval);
            }
        } else if(currentAction is TargetProjectile tp) {
            approachEnemy = false;

            actionTarget = tp.isFriendly == true ? targetedAlly : targetedEnemy;
            Invoke("CastSpell", currentAction.castDelay);
            ResetInterval(currentAction.actionInterval);
        }
    }

    void CastSpell() {
        currentAction.Cast(actionTarget, firePoint);
    }

    public void ResetInterval(float time) {
        actionIntervalTimer = time;
        readyToCast = true;
    }

    void ApproachEnemy() {
        if(Vector3.Distance(transform.position, targetedEnemy.position) > approachEnemyRange) {
            agent.SetDestination(actionTarget.position);
        } else {
            MakeAction();
        }
    }

    void SpaceEnemy() {
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
