using UnityEngine;
using UnityEngine.AI;

public class SpellCasting : MonoBehaviour
{
    private bool readyToCast, approachTarget, targetInSight, tryRecast, canMove;
    private Transform actionTarget, targetedEnemy, targetedAlly;
    private float approachTargetRange;
    private Action currentAction;
    private LevelInfo levelInfo;
    private NavMeshAgent agent;

    [SerializeField] private string attackActionTag;
    [SerializeField] private string supportActionTag;
    [SerializeField] private Action[] actionListAttack;
    [SerializeField] private Action[] actionListSupport;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float spaceEnemyRange;
    [SerializeField] private float sightLength;
    [SerializeField] private LayerMask whatToSee;

    private void Start() {
        readyToCast = true;
        agent = GetComponent<NavMeshAgent>();
        targetedAlly = FindTarget.ClosestTo(supportActionTag, transform);
        targetedEnemy = FindTarget.ClosestTo(attackActionTag, transform);
        levelInfo = LevelInfo.instance;
        
        RollAction();
    }

    private void Update() {
        // SEARCHING FOR A TARGET
        if(transform.CompareTag("Hero")) {
            if(targetedEnemy == null && levelInfo.EnemyCount > 0) {
                targetedEnemy = FindTarget.ClosestTo(attackActionTag, transform);
            }

            if(targetedAlly == null && levelInfo.AllyCount > 0) {
                targetedAlly = FindTarget.ClosestTo(supportActionTag, transform);
            }
        } else {
            if(targetedAlly == null && levelInfo.EnemyCount > 0) {
                targetedAlly = FindTarget.ClosestTo(supportActionTag, transform);
            }
        }
        
        if(actionTarget == null) {
            GetActionTarget();
        }

        // LINE OF SIGHT
        if(actionTarget != null) {
            if(Physics.Raycast(transform.position, actionTarget.position - transform.position, out RaycastHit sightHit, sightLength, whatToSee, QueryTriggerInteraction.Ignore)) {
                if(sightHit.transform == actionTarget.transform) {
                    targetInSight = true;

                    if(currentAction is AOE == false) {
                        approachTarget = false;

                        if(tryRecast) {
                            tryRecast = false;
                            MakeAction();
                        }
                    }
                } else {
                    targetInSight = false;
                    approachTarget = true;
                }
            } 
        }
        
        // MAKING ACTION IF ABLE
        if(readyToCast && actionTarget != null) {
            canMove = true;
            MakeAction();
        } else {
            GetActionTarget();
        }

        // HERO MOVEMENT
        if(canMove) {
            if(approachTarget) {
                ApproachTarget();
            } else{
                SpaceTarget();
            } 
        } else {
            agent.SetDestination(transform.position);
        }
    }

    private void MakeAction() {
        readyToCast = false;

        if(currentAction is AOE aoe) {
            if(Vector3.Distance(transform.position, actionTarget.position) > aoe.range - 1) {
                approachTarget = true;
                approachTargetRange = aoe.range - 1;
            } else {
                approachTarget = false;
                canMove = false;

                Invoke(nameof(CastSpell), currentAction.castDelay);
            }
        } else {
            if(targetInSight) {
                approachTarget = false;
                Invoke(nameof(CastSpell), currentAction.castDelay);
            } else {
                tryRecast = true;
            }
        }
    }

    private void CastSpell() {
        if(currentAction is TargetSpell) {
            currentAction.Cast(actionTarget); 
        } else {
            currentAction.Cast(actionTarget, firePoint);
        }

        actionTarget = null;
        Invoke(nameof(ResetInterval), currentAction.actionInterval);
        RollAction();
    }

    private void ResetInterval() {
        readyToCast = true;
    }

    private void RollAction() {
        if(targetedAlly.GetComponent<HealthSystem>().InHealingRange() && actionListSupport.Length != 0) {
            currentAction = Random.Range(0, 2) == 0 ? actionListAttack[Random.Range(0, actionListAttack.Length)] : actionListSupport[Random.Range(0, actionListSupport.Length)];
        } else {
            currentAction = actionListAttack[Random.Range(0, actionListAttack.Length)];
        }

        GetActionTarget();
    }

    private void GetActionTarget() {
        actionTarget = currentAction is TargetSpell ? targetedAlly : targetedEnemy;
    }

    private void ApproachTarget() {
        if(Vector3.Distance(transform.position, targetedEnemy.position) > approachTargetRange && actionTarget != null) {
            agent.SetDestination(actionTarget.position);
        } else {
            MakeAction();
        }
    }

    private void SpaceTarget() {
        if (actionTarget == null) return;
        if(Vector3.Distance(transform.position, actionTarget.position) < spaceEnemyRange) {
            Vector3 target = (transform.position - actionTarget.position).normalized;
            agent.SetDestination(transform.position + target);
        } else if(Vector3.Distance(transform.position, actionTarget.position) > spaceEnemyRange + 2) {
            agent.SetDestination(actionTarget.position);
        } else {
            agent.SetDestination(transform.position);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        if(actionTarget != null)
            Gizmos.DrawRay(transform.position, actionTarget.position - transform.position);
    }
}
