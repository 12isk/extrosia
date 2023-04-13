using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactVFX;
    public bool collided;
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
}
