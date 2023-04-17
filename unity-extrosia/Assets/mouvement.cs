using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement : MonoBehaviour
{
    public float forceMagnitude = 10f;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * forceMagnitude;

        // applique la force au rigidbody pour faire bouger l'objet
        rb.AddForce(randomForce, ForceMode.Force); 
    }
}
