using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactVFX;
    public bool collided;
    public float launchTime;
    
    public float damage = 0.1f;
    
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    { 
        if (!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;

            var impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;
            
            Destroy(impact, 2);
            Destroy(gameObject);
        }
    }

    public void Start()
    { 
        launchTime = Time.time;
    }
    public void Update()
    {
        if (Time.deltaTime - launchTime >= 25f)
        {
            Destroy(this);
        }
    }
}
