using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuy : MonoBehaviour
{

    public GameObject[] PatrolWaypoints;
    public float Speed;

    private Transform currentTargetTransform;

    private int currentTargetIndex;
    private EnemyState state;
    private GameObject player;
    private bool pause;
    private DateTime startPause;

    private enum EnemyState
    {
        Patrol,
        Chase,
        ReturnPatrol
    }

    // Use this for initialization
    void Start()
    {
        StartPatrol();
        player = GameObject.Find("Player");
    }

    internal void VisionConeCollided(Collider2D other)
    {
        if (state != EnemyState.Chase)
        {
            PauseForChase();
            StartChase(other.transform);
        }
    }

    private void PauseForChase()
    {
        pause = true;
        startPause = DateTime.UtcNow;
    }

    private void StartChase(Transform target)
    {
        state = EnemyState.Chase;
        currentTargetIndex = 0;
        currentTargetTransform = target;
    }

    private void StartPatrol()
    {
        state = EnemyState.Patrol;
        currentTargetIndex = 0;
        currentTargetTransform = PatrolWaypoints[currentTargetIndex].transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (state)
        {
            case EnemyState.Patrol:
                if (Vector3.Distance(transform.position, currentTargetTransform.position) < 0.1)
                    currentTargetTransform = NextPatrolWaypoint();
                break;
            case EnemyState.Chase:
                var distance = Vector3.Distance(transform.position, currentTargetTransform.position);
                if (distance > 5)
                    StartPatrol();
                if (distance < 0.2)
                    GameManager.Instance.PlayerKilled();
                break;
            default:
                break;
        }

        if (!pause)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentTargetTransform.position, step);
        }
        else if ((DateTime.UtcNow - startPause).TotalSeconds > 0.5)
            pause = false;

        transform.right = currentTargetTransform.position - transform.position;


    }

    private Transform NextPatrolWaypoint()
    {
        currentTargetIndex++;
        if (currentTargetIndex > PatrolWaypoints.Length - 1) currentTargetIndex = 0;
        return PatrolWaypoints[currentTargetIndex].transform;
    }
}
