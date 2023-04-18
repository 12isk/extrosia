using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Camera cam;

    private Vector3 destination;
    
    public GameObject lowAtt;
    public GameObject HiAtt;

    public float projSpeed = 30f;
    public float fireRate = 4f;
    private float timeToFire;

    public float arcRange = 1f;
    public Transform firepoint;
    
    public Rigidbody rb;
    public 

    
    
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile(1);
        }
        if (Input.GetButtonDown("Fire2") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile(2);
        }
    }

    private void ShootProjectile(int choice)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(100);
        }

        InstantiateProjectile(firepoint,choice);
        
    }

    // Update is called once per frame
    void InstantiateProjectile(Transform firepoint, int choice)
    {
        if (choice == 1)
        {
            var hiAttObj = Instantiate(lowAtt, firepoint.position, Quaternion.identity) as GameObject;
            hiAttObj.GetComponent<Rigidbody>().velocity = (destination - firepoint.position).normalized * projSpeed;
            iTween.PunchPosition(hiAttObj,
                new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0),
                Random.Range(0.5f,2f));
        }

        else
        {
            var hiAttObj = Instantiate(HiAtt, firepoint.position, Quaternion.identity) as GameObject;
            hiAttObj.GetComponent<Rigidbody>().velocity = (destination - firepoint.position).normalized * projSpeed;
            iTween.PunchPosition(hiAttObj,
                new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0),
                Random.Range(0.5f,2f));
        }
        
    }

} 
