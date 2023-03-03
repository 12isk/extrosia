using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump3 : MonoBehaviour
{
    public float jumpSpeed = 10f;
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
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        } 
    }
}
