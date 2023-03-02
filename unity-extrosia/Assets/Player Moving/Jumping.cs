using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour{

//jumping mechanics
    [SerializeField] public float jumpHeight = 8f;
    private bool isFalling = false;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isFalling==false)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isFalling = true;
        }
    }
}
