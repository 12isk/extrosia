using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Camera cam;

    private Vector3 destination;
    
    public GameObject projectile1;
    public GameObject projectile2;
    
    public float projSpeed = 30f;
    public float fireRate = 4f;
    private float timeToFire;

    public float arcRange = 1f;
    public Transform firepoint;
    public float launchTime; 
    public Rigidbody rb;
     

    
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire)
        {
            launchTime = Time.time;
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile(1);
        }
        if (Input.GetButtonDown("Fire2") && Time.time >= timeToFire)
        {
            launchTime = Time.time;
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile(2);
        }
        
        
        
    }

    private void ShootProjectile(int choice=0)
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

        InstantiateProjectile(firepoint, choice);
        
        
    }
    
    

    // Update is called once per frame
    void InstantiateProjectile(Transform firepoint, int choice)
    {
        GameObject projectile = default;
        
        switch (choice)
        {
           case 1:
                projectile = projectile1; 
                break;
           case 2:
               projectile = projectile2;
               break;
        }
        
        var projectileObj = Instantiate(projectile, firepoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firepoint.position).normalized * projSpeed;
        iTween.PunchPosition(projectileObj,
            new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0),
            Random.Range(0.5f,2f));
    }

} 
