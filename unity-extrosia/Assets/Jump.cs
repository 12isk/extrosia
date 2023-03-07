using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce = 500;
    public float jumpHeight;
    public float speed = 6f;
    public float fallMultiplier = 2.5f;
    private bool canJump;
    
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();

        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * (Input.GetAxis("Horizontal") * speed));
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {   
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump=false;
        }
    }

    public void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * (Physics.gravity.y * fallMultiplier * Time.deltaTime);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        canJump = true;
    }

    private void OnCollisionExit(Collision other)
    {
        canJump = false;
    }

    float CalculateJumpForce(float mass, float height)
    {
        // Calculate the time it takes for the object to reach the peak of the jump
        float timeToPeak = Mathf.Sqrt(2f * height / Mathf.Abs(Physics.gravity.y));

        // Calculate the force required to achieve the desired jump height
        float force = mass * Mathf.Abs(Physics.gravity.y) * Mathf.Pow(timeToPeak, 2f);

        return force;
    }
}
