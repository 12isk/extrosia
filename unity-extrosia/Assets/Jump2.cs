using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump2 : MonoBehaviour
{
     public float jumpForce = 1f;
    private bool canJump;
    // Start is called before the first frame update
    void Start()
    {
       canJump = true; 
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space) & canJump)
        {   
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        } 
    }
}
