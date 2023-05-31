using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WInText : MonoBehaviour
{
    
    [Header("Component")] public TextMeshProUGUI winText;
    public Stats stats;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (stats.hasWon)
     {
         winText.text = "You won";
     }
     else
     {
         winText.text = "You lost";
     }
    }
}
