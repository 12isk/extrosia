using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public bool isActive;
    
    [SerializeField] public string title;
    [SerializeField] public string description;
    [SerializeField] public int experienceReward;
    [SerializeField] public int goldReward;
    [SerializeField] public int questID;

    
}
