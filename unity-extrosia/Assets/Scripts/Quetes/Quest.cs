using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public bool isActive;
    
    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;
    public int questID;

    
}
