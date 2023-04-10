using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public float jumpForce = 500;
    public float jumpHeight;
    public float speed = 6f;
    public float fallMultiplier = 2.5f;
    public float jumpButtonGracePeriod;
    public float jumpButtonPressedTime;
    public float lastGroundedTime;
    private bool _canJump;
    private bool isJumping;
    private bool isFalling;
    private bool isGrounded;
    private float yVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
         animator = GetComponent<Animator>();
         _canJump = true;
    }

    // Update is called once per frame
    void Update()
    {

        // Je save le last grounded time pour gerer les appuis desynchronises
        
        if (_canJump)
        {
            lastGroundedTime = Time.time;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpButtonPressedTime = Time.time;
            
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            animator.SetBool("isGrounded",true);
            isGrounded = true;
            animator.SetBool("isJumping",false);
            isJumping = false;
            animator.SetBool("isFalling",false);
            isFalling = false;

            
            if (Time.time > 0.1 && Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod )
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpButtonPressedTime = default;
                lastGroundedTime = default;
                animator.SetBool("isGrounded",false);
                isGrounded = false;
                animator.SetBool("isJumping", true);
                isJumping = true;
                _canJump = false;

            }
        }
        else
        {
            animator.SetBool("isGrounded", false);
            isGrounded = false;
            if ((isJumping && rb.velocity.y < 0) || rb.velocity.y < -1)
            {
                animator.SetBool("isFalling",true);
            }
            
            
        }
        if ((isJumping && rb.velocity.y < 0) || rb.velocity.y < -1)
        {
            animator.SetBool("isGrounded", false);
            isGrounded = false;
            animator.SetBool("isFalling",true);
        }
        // TODO: USE RAYS   
        
    }

    public void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * (Physics.gravity.y * fallMultiplier * Time.deltaTime);
                isFalling = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        _canJump = true;
    }

    private void OnCollisionExit(Collision other)
    {
        _canJump = false;
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
