using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// public class EnemyAI : MonoBehaviour
// {
//     private enum State
//     {
//         Roaming,
//         ChaseTarget,
//         ShootingTarget,
//         GoingBackToStart,
//     }
//
//     public Transform player;
//     public NavMeshAgent agent;
//     private Vector3 startingPosition;
//     private Vector3 roamPosition;
//
//     public int health;
//     public int maxHealth;
//     public HealthBar healthBar;
//
//     public float speed = 40f;
//     public float attackRange = 30f;
//     public float stopChaseDistance = 80f;
//     public float reachedPositionDistance = 10f;
//
//     private State state;
//     public GameObject projectile;


    public class EnemyAI : MonoBehaviour
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
        public GameObject projectile;

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
                        Rigidbody rb = Instantiate(projectile, transform.position, quaternion.identity)
                            .GetComponent<Rigidbody>();

                        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                        rb.AddForce(transform.up * 32f, ForceMode.Impulse);

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


//     private void Awake() {
//         state = State.Roaming;
//     }
//     private void Start() {
//         startingPosition = transform.position;
//         roamPosition = GetRoamingPosition();
//         healthBar.SetMaxHealth(maxHealth);
//         health = maxHealth;
//     }
//      private void Update() 
//      {
//          
//          healthBar.SetHealth(health);
//          
//          
//          
//         switch (state) {
//         default:
//         case State.Roaming:
//             agent.SetDestination(roamPosition);
//
//             if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
//                 // Reached Roam Position
//                 roamPosition = GetRoamingPosition();
//             }
//
//             FindTarget();
//             break;
//         case State.ChaseTarget:
//             agent.SetDestination(player.position);
//
//             if ((Vector3.Distance(transform.position, player.position)) < attackRange) {
//                 // Target within attack range
//                 //Attack code
//                 // Rigidbody rb = Instantiate(projectile, transform.position, quaternion.identity).GetComponent<Rigidbody>();
//                 //
//                 // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
//                 // rb.AddForce(transform.up * 32f, ForceMode.Impulse);
//                 //
//                 // projectile.GetComponent<Rigidbody>().velocity = (player.position - transform.position).normalized * speed;
//                 // iTween.PunchPosition(projectile,
//                 //     new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0),
//                 //     Random.Range(0.5f,2f));
//
//                 
//                 InstantiateProjectile();
//             Debug.Log("Attack");
//                 // Attack Code end 
//             }
//
//             
//             if (Vector3.Distance(transform.position, player.position) > stopChaseDistance) {
//                 // Too far, stop chasing
//                 state = State.GoingBackToStart;
//             }
//             break;
//         case State.ShootingTarget:
//             break;
//         case State.GoingBackToStart:
//             agent.SetDestination(startingPosition);
//             
//             reachedPositionDistance = 10f;
//             if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
//                 // Reached Start Position
//                 state = State.Roaming;
//             }
//             break;
//         }
//     }
//
//      
//      void InstantiateProjectile()
//      {
//          // Instantiate the projectile with speed and direction
//          var projectileObj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
//          projectileObj.GetComponent<Rigidbody>().velocity = (player.position - transform.position).normalized * speed;
//          iTween.PunchPosition(projectileObj,
//              new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0),
//              Random.Range(0.5f,2f));
//      }
//     private Vector3 GetRoamingPosition() {
//         return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
//     }
//
//     private static Vector3 GetRandomDir()
//     {
//         return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
//     }
//
//     private void FindTarget() {
//         float targetRange = 50f;
//         if (Vector3.Distance(transform.position, player.position) < targetRange) {
//             // Player within target range
//             state = State.ChaseTarget;
//         }
//     }
//     
//     private void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.red;
//         var position = transform.position;
//         Gizmos.DrawWireSphere(position, attackRange);
//         Gizmos.color = Color.yellow;
//         Gizmos.DrawWireSphere(position, stopChaseDistance);
//
//     }
//     
//     private void TakeDamage(int damage)
//     {
//         health -= damage;
//         if (health <= 0)
//         {
//             Destroy(gameObject);
//         }
//     }
//     private void OnCollisionEnter(Collision collision)
//     {
//         
//         if (collision.gameObject.CompareTag("Bullet"))
//         {
//             var bullet = collision.gameObject.GetComponent<Projectile>();
//             int damage = bullet.damage;
//             TakeDamage(damage);
//         }
//         
//         if (collision.gameObject.CompareTag("Enemy"))
//         {
//             Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
//         }
//         
//     }
// }
