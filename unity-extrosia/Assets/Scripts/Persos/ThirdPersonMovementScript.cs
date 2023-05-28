using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovementScript : MonoBehaviour
{
    // Used as a reference for the camera
    public Transform cam;
    
    public CharacterController controller;
    public float speed= 6f;
    public float turnSmoothTime = 0.25f;
    
    float velocity;

    private Rigidbody rb;

    private Animator _animator;
     public float rotationSpeed = 180f;
    // public float stabilization;
    // public float damping;
    
    //smoothing 
    private float smoothVelocity;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int isRunning = Animator.StringToHash("isRunning");
    private static readonly int Dodge = Animator.StringToHash("Dodge");


    // Update is called once per frame


   //input stuff
   void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }
   
//    void FixedUpdate()
//    {
//        // // Calculate the rotation the object should have to be upright
//        // Quaternion targetRotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
//        //
//        // // Smoothly rotate the object towards the target rotation
//        // rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed));

//        Vector3 torque = stabilization * Vector3.Cross(transform.up, Vector3.up) - damping * rb.angularVelocity;
//        rb.AddTorque(torque.normalized);
//    }

    void Update()
    {
        
        if (Input.GetButtonDown("Run"))
        {
            _animator.SetBool(isRunning, true);
            speed = 19f;
        }
        else if (Input.GetButtonUp("Run"))
        {
            _animator.SetBool(isRunning, false);
            speed = 6f;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetBool(Dodge,true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            _animator.SetBool(Dodge,false);
        }
        
        
        // getting our input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // a Vector containing the direction of the movement
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        
        velocity = (Vector3.forward * (speed * (vertical + horizontal))).magnitude; 
        
        // didnt really get this part, will understand later 
        // todo: explain this in more detail
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            // smoothing out the the turning angle so the movement isnt snappy
            float angle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
            
            //the actual movement
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            // Creating a new moving direction while taking into account the cam mvmt
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            
            
            
            //transition for the animator

            controller.Move(moveDir.normalized * (speed * Time.deltaTime));
        }
        
        
        _animator.SetFloat(Speed, velocity);


        
        
    }




    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    //    
    //    }
    //
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        transform.Translate(Vector3.back * Time.deltaTime * speed);
    //    }
    //
    //    transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
    //}
    
    
}
