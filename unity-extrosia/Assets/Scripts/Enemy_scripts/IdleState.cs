using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeeThePlayer;
    
    public UnityEngine.AI.NavMeshAgent agent;
    
    public Transform player;
    
    public LayerMask whatIsGround, whatIsPlayer;
    public float sightRange, attackRange;
    
    // Patrolling

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
        {
            canSeeThePlayer = true;
        }
        else
        {
            canSeeThePlayer = false;
        }
    }

    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
            Patrolling();
            return this;
        }
    }
    public void Patrolling()
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
}
