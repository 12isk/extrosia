using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
public class NPC_RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //range of movement

    public Transform centrePoint; //centre of the area the agent wants to move around in

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.CompareTag)
    }
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
                agent.SetDestination(point);
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in the range
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    
}