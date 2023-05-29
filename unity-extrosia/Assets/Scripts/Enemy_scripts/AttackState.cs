using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackState : State
{
    public UnityEngine.AI.NavMeshAgent agent;
    
    public Transform player;
    
    //Attacking

    public float timeBetweenAttack;
    private bool alreadyAttacked;
    public GameObject projectile;

    public override State RunCurrentState()
    {
        Debug.Log("Attacked");
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
        return this;
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
