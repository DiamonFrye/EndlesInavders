using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    public int shootPercentChance = 50;
    public float nextShotAttemptTime = 0.5f;
    public float nextShotAttemptWaiting = 1f;
    public Transform bulletPrefab;
    public float effectSpownRate = 10;
    public bool firePremission = false;

    private Transform firePoint;

    // Use this for initialization
    void Start()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("FirePoint point to null. (No FirePoint)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextShotAttemptTime && firePremission == true)
        {
            nextShotAttemptTime = Time.time + nextShotAttemptWaiting;
            int shootChance = Random.Range(0, 100);
            if(shootChance <= shootPercentChance)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
