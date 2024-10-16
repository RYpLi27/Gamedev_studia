using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroSpellSystem : MonoBehaviour
{
    private Transform targetedEnemy;
    private bool approachEnemy, readyToCast;
    private Rigidbody rb;
    private float actionIntervalTimer;
    private Action currentAction;

    [Header("Stats")]
    [SerializeField] private float moveSpeed;

    [Header("Transforms")]
    [SerializeField] private Transform firePoint;

    [Header("Actions")]
    [SerializeField] private Action[] actionList;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        readyToCast = true;
    }

    void Update()
    {
        if(targetedEnemy == null) {
            targetedEnemy = FindFirstObjectByType<Enemy>().transform;
        }

        if(actionIntervalTimer > 0) {
            actionIntervalTimer -= Time.deltaTime;
        } else if(readyToCast == true) {
            currentAction = actionList[Random.Range(0, actionList.Length)];
            MakeAction();
        }

        if(approachEnemy == true) {
            ApproachEnemy();
        }
    }

    void MakeAction() {
        readyToCast = false;
        if(currentAction.name == "AOE") {
            if(Vector3.Distance(transform.position, targetedEnemy.position) > 2.5f) {
                approachEnemy = true;
            } else {
                approachEnemy = false;
                currentAction.Cast(targetedEnemy, firePoint);
                ResetInterval(currentAction.actionInterval);
            }
        } else {
            currentAction.Cast(targetedEnemy, firePoint);
            ResetInterval(currentAction.actionInterval);
        }
    }

    public void ResetInterval(float time) {
        actionIntervalTimer = time;
        readyToCast = true;
    }

    void ApproachEnemy() {
        if(Vector3.Distance(transform.position, targetedEnemy.position) > 2.5f) {
                transform.position = Vector3.MoveTowards(transform.position, targetedEnemy.position, moveSpeed * Time.deltaTime);
            } else {
                MakeAction();
            }
    }
}
