using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public Transform Guide; 


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 3f;
            Collider[] colliderArr = Physics.OverlapSphere(transform.position, interactRange);

            foreach (var collider in colliderArr)
            {
                Debug.Log(collider);
            }
        }
        
    }
}
