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
        
        public float attackRange = 30f;
        public float stopChaseDistance = 80f;
        
        public Transform firepoint;
        public GameObject projectile;

        public float launchTime;

        public float fireRate= 2.5f;
        
        public float health;
        public float maxHealth;
        public HealthBar healthBar;
        
        private void Awake()
        {
            state = State.Roaming;
        }

        private void Start()
        {
            startingPosition = transform.position;
            roamPosition = GetRoamingPosition();
            
            health = maxHealth;
        }

        private void Update()
        {
            
            healthBar.SetHealth(health);
            
            
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
//                    float attackRange = 30f;

                    if ((Vector3.Distance(transform.position, player.position)) < attackRange)
                    {
                        InstantiateProjectile();
                                              
                    }

                    //float stopChaseDistance = 80f;
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

        private void InstantiateProjectile()
        {


            Debug.Log("Firing");
            launchTime = Time.time;
            var projectileObj = Instantiate(projectile, firepoint.position, quaternion.identity);
            projectileObj.GetComponent<Rigidbody>().velocity = (player.position - firepoint.position).normalized * 35f;
            iTween.PunchPosition(projectileObj,
                new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0),
                Random.Range(0.5f, 2f));
        }
        
        
        
        private void TakeDamage(float damage)
     {
         health -= damage;
         if (health <= 0)
         {
             Destroy(gameObject);
         }
     }
     private void OnCollisionEnter(Collision collision)
     {
         
         if (collision.gameObject.CompareTag("Bullet"))
         {
             var bullet = collision.gameObject.GetComponent<Projectile>();
             float damage = bullet.damage;
             TakeDamage(damage);
         }
         
         if (collision.gameObject.CompareTag("Enemy"))
         {
             Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
         }
         
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
        
             private void OnDrawGizmosSelected()
     {
         Gizmos.color = Color.red;
         var position = transform.position;
         Gizmos.DrawWireSphere(position, attackRange);
         Gizmos.color = Color.yellow;
         Gizmos.DrawWireSphere(position, stopChaseDistance);

     }
}
