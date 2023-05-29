using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public bool isInAttackRange;
    public UnityEngine.AI.NavMeshAgent agent;
    
    public Transform player;
    public float sightRange, attackRange;
    public LayerMask whatIsGround, whatIsPlayer;
    /*private void Update()
    {
        if (Physics.CheckSphere(transform.position, attackRange, whatIsPlayer))
        {
            isInAttackRange = true;
        }
        else
        {
            isInAttackRange = false;
        }
    }*/

    public override State RunCurrentState()
    {
        if (Physics.CheckSphere(transform.position, attackRange, whatIsPlayer))
        {
            return attackState;
        }
        else
        {
            agent.SetDestination(player.position);
            return this;
        }
    }
}
