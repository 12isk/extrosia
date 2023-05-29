using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private enum State {
        Roaming,
        ChaseTarget,
        ShootingTarget,
        GoingBackToStart,
    }
    public Transform player;
    public NavMeshAgent agent;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private State state;
    public GameObject projectile;
    private void Awake() {
        state = State.Roaming;
    }
    private void Start() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }
     private void Update() {
        switch (state) {
        default:
        case State.Roaming:
            agent.SetDestination(roamPosition);

            float reachedPositionDistance = 10f;
            if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                // Reached Roam Position
                roamPosition = GetRoamingPosition();
            }

            FindTarget();
            break;
        case State.ChaseTarget:
            agent.SetDestination(player.position);

            float attackRange = 30f;
            if ((Vector3.Distance(transform.position, player.position)) < attackRange) {
                // Target within attack range
                //Attack code
                Rigidbody rb = Instantiate(projectile, transform.position, quaternion.identity).GetComponent<Rigidbody>();
            
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 32f, ForceMode.Impulse);

                // Attack Code end 
            }

            float stopChaseDistance = 80f;
            if (Vector3.Distance(transform.position, player.position) > stopChaseDistance) {
                // Too far, stop chasing
                state = State.GoingBackToStart;
            }
            break;
        case State.ShootingTarget:
            break;
        case State.GoingBackToStart:
            agent.SetDestination(startingPosition);
            
            reachedPositionDistance = 10f;
            if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
                // Reached Start Position
                state = State.Roaming;
            }
            break;
        }
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    private static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void FindTarget() {
        float targetRange = 50f;
        if (Vector3.Distance(transform.position, player.position) < targetRange) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }

}
