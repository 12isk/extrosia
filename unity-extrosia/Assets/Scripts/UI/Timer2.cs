using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer2 : MonoBehaviour
{
    [Header("Component")] public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
       // timerText.text = currentTime.ToString("0.00");
       var text = string.Format("{0:0}:{1:00}", MathF.Floor(currentTime / 60), currentTime % 60);
       timerText.text = text;

    }
}
