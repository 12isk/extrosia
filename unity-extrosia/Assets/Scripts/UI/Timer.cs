using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

// public class Timer : MonoBehaviour
// {
//     float time;
//     public float TimerInterval = 5f ;
//     float tick;
//
//     void Awake()
//     {
//         time = (int)Time.time;
//         tick = TimerInterval;
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//
//         GetComponent<MediaTypeNames.Text> ().text = string.Format("{0:0}:{1:00}",MathF.Floor(time/60),time%60);
//         time = (int)Time.time;
//
//         if (time == tick)
//         {
//             tick = time + TimerInterval;
//             Debug.Log ("Timer");
//         }
//     }
//}
