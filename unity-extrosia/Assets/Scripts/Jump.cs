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
    public float groundCheckDistance = 0.1f;
    private bool _canJump;
    private bool isJumping;
    private bool isFalling;
    private bool isGrounded;
    [SerializeField] public float yVelocity;

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
        Debug.Log(rb.velocity.y);
        Debug.Log(isFalling);


        //checking if bro is on the ground
        if (Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance))
        {
            // Player is grounded
            isGrounded = true;
            lastGroundedTime = Time.time;
            _canJump = true;
        }
        else
        {
            // Player is not grounded
            isGrounded = false;
            _canJump = false;
        }
        // Je save le last grounded time pour gerer les appuis desynchronises

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpButtonPressedTime = Time.time;

        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod && isGrounded)
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJumping = false;
            animator.SetBool("isFalling", false);
            isFalling = false;


            if (Time.time > 0.1 && Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                jumpButtonPressedTime = default;
                lastGroundedTime = default;
                animator.SetBool("isGrounded", false);
                isGrounded = false;
                animator.SetBool("isJumping", true);
                isJumping = true;
                _canJump = false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                

            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            yVelocity = rb.velocity.y;
            if ((isJumping && yVelocity <= 0.1) | yVelocity < -1 | !_canJump)
            {
                animator.SetBool("isJumping", false);
                isJumping = false;
                animator.SetBool("isFalling", true);
                isFalling = true;
            }


        }

        yVelocity = rb.velocity.y;



    }

    public void FixedUpdate()
    {
        if (!_canJump)
        {
            if (rb.velocity.y < 0.1)
            {
                rb.velocity += Vector3.up * (Physics.gravity.y * fallMultiplier * Time.deltaTime);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                isJumping = false;
                isFalling = true;
            }
        }
    }
}

//     private void OnCollisionStay(Collision collision)
//     {
//         _canJump = true;
//     }
//
//     private void OnCollisionExit(Collision other)
//     {
//         _canJump = false;
//     }
//
//     float CalculateJumpForce(float mass, float height)
//     {
//         // Calculate the time it takes for the object to reach the peak of the jump
//         float timeToPeak = Mathf.Sqrt(2f * height / Mathf.Abs(Physics.gravity.y));
//
//         // Calculate the force required to achieve the desired jump height
//         float force = mass * Mathf.Abs(Physics.gravity.y) * Mathf.Pow(timeToPeak, 2f);
//
//         return force;
//     }
// }
