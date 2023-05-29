using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Lorax : MonoBehaviour
{
    public Collider innerCollider;
    public Canvas canvas;
    private CanvasManager canvasManager;
    // Start is called before the first frame update

    private void Start()
    {
        canvasManager = canvas.GetComponent<CanvasManager>();
        canvasManager.DisableCanvas();
    }   
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            canvasManager.EnableCanvas();
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvasManager.DisableCanvas();
        }
    }
    
}
