using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jummmp : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public float jumpDuration = 1f;

    private bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpCoroutine());
        }
    }
    IEnumerator JumpCoroutine()
    {
    
        isJumping = true;

        float elapsedTime = 0.0f;
        Vector3 startingPosition = transform.position;

        // Jump up
        while (elapsedTime < jumpDuration / 1.5)
        {
            transform.position += Vector3.up * jumpForce * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Jump down
        while (elapsedTime < jumpDuration)
        {
            transform.position -= Vector3.up * jumpForce * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startingPosition;
        isJumping = false;
    }
}
