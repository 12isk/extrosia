using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public Drops ManaDrop;
    public Drops HealthDrop;
    public Drops ExpDrop;
    
    public GameObject explosion;
    
    // Start is called before the first frame update
    // void OnDestroy()
    // {
    //     if (this!.gameObject.scene.isLoaded) return;
    //         
    //     Instantiate(ManaDrop, transform.position, Quaternion.identity);
    //     Instantiate(HealthDrop, transform.position, Quaternion.identity);
    //     Instantiate(ExpDrop, transform.position, Quaternion.identity);
    //     
    //     
    //     
    //     //Instantiate(explosion, transform.position, Quaternion.identity);
    // }

     public void Destroying(char c)
    {
        if (c == 'd')
        {
            Destroy(this);
            if (this!.gameObject.scene.isLoaded) return;
            
            Instantiate(ManaDrop, transform.position, Quaternion.identity);
            Instantiate(HealthDrop, transform.position, Quaternion.identity);
            Instantiate(ExpDrop, transform.position, Quaternion.identity);
        }

        else
        {
            Destroy(this);
        }
    }
    private void OnDisable()
    {
        Destroying('c');
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
