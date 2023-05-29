using System;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas canvas;
    

    // Call this method when you want to enable the canvas
    public void EnableCanvas()
    {
        canvas.enabled = true;
        print("has entered");
    }
    
    // Call this method when you want to disable the canvas
    
    public void DisableCanvas()
    {
        canvas.enabled = false;
    }
}

