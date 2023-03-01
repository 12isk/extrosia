using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementScript : MonoBehaviour
{
    // Used as a reference for the camera
    public Transform cam;
    
    public CharacterController controller;
    public float speed= 6f;
    public float turnSmoothTime = 0.1f;

    private float smoothVelocity;
    // Update is called once per frame
    void Update()
    {
        // getting our input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // a Vector containing the direction of the movement
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
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
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
