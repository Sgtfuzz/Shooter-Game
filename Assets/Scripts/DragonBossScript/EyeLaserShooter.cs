using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaserShooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform eye;

    float fireRate;
    float nextFire;

    
    void Awake()
    {
        fireRate = 1.5f;
        nextFire = Time.time;

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
