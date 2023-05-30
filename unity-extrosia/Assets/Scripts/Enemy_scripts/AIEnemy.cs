using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIEnemy : MonoBehaviour
{
    private enum State
        {
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

        public Transform firepoint;
        public GameObject projectile;


        public int fireRate;
        
        private void Awake()
        {
            state = State.Roaming;
        }

        private void Start()
        {
            startingPosition = transform.position;
            roamPosition = GetRoamingPosition();
        }

        private void Update()
        {
            switch (state)
            {
                default:
                case State.Roaming:
                    agent.SetDestination(roamPosition);

                    float reachedPositionDistance = 10f;
                    if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                    {
                        // Reached Roam Position
                        roamPosition = GetRoamingPosition();
                    }

                    FindTarget();
                    break;
                case State.ChaseTarget:
                    agent.SetDestination(player.position);

                    float attackRange = 30f;
                    if ((Vector3.Distance(transform.position, player.position)) < attackRange)
                    {
                        // Target within attack range
                        //Attack code
                        var projectileObj = Instantiate(projectile, firepoint.position, quaternion.identity);
        
                        
                        projectileObj.GetComponent<Rigidbody>().velocity = (player.position - firepoint.position).normalized * 35f;
                        iTween.PunchPosition(projectileObj,
                            new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0),
                            Random.Range(0.5f,2f));
                        // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                        // rb.AddForce(transform.up * 32f, ForceMode.Impulse);
                        //rb.velocity = (player.position - (transform.position+transform.up)).normalized * 35f;
                        // Attack Code end 
                    }

                    float stopChaseDistance = 80f;
                    if (Vector3.Distance(transform.position, player.position) > stopChaseDistance)
                    {
                        // Too far, stop chasing
                        state = State.GoingBackToStart;
                    }

                    break;
                case State.ShootingTarget:
                    break;
                case State.GoingBackToStart:
                    agent.SetDestination(startingPosition);

                    reachedPositionDistance = 10f;
                    if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
                    {
                        // Reached Start Position
                        state = State.Roaming;
                    }

                    break;
            }
        }

        private Vector3 GetRoamingPosition()
        {
            return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
        }

        private static Vector3 GetRandomDir()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        private void FindTarget()
        {
            float targetRange = 50f;
            if (Vector3.Distance(transform.position, player.position) < targetRange)
            {
                // Player within target range
                state = State.ChaseTarget;
            }
        }
}
