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
    void OnDestroy()
    {
        Instantiate(ManaDrop, transform.position, Quaternion.identity);
        Instantiate(HealthDrop, transform.position, Quaternion.identity);
        Instantiate(ExpDrop, transform.position, Quaternion.identity);
        
        //Instantiate(explosion, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
