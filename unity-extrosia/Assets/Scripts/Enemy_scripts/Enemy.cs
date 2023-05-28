// ReSharper disable RedundantUsingDirective
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    
    public Transform player;
    
    public LayerMask whatIsGround, whatIsPlayer;

    //health bar
    public float maxHealth;
    public float health;
    
    public HealthBar healthBar;
    // Patrolling

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    //Attacking

    public float timeBetweenAttack;
    private bool alreadyAttacked;
    public GameObject projectile;
    
    //States

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    
    // Drops 
    public GameObject explosion;
    public GameObject healthDrop;
    public GameObject manaDrop;

    private void Awake()
    {
        player = GameObject.Find("Eva - Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }


    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        playerInSightRange = Physics.CheckSphere(position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        
    }



    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Destination reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make sure player doesn't move 
        agent.SetDestination(transform.position);
        //turn towards player
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code
            Rigidbody rb = Instantiate(projectile, transform.position, quaternion.identity).GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 32f, ForceMode.Impulse);

            // Attack Code end 
            
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);
        }
        
        healthBar.SetHealth(health);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        var position = transform.position;
        Instantiate(explosion, position, quaternion.identity);
        Instantiate(healthDrop, position, quaternion.identity);
        Instantiate(manaDrop, position, quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            var bullet = collision.gameObject.GetComponent<Projectile>();
            int damage = bullet.damage;
            TakeDamage(damage);
        }
        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, sightRange);

    }
}
