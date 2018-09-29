using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public float fireRate = 5;
    public Transform bulletPrefab;
    public float effectSpownRate = 10;
//    public Transform muzzleFlashPrefab;

//    private float timeToSpawnEffect = 0;
    private float timeToFire = 0;
    private Transform firePoint;

	// Use this for initialization
	void Start () {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("FirePoint point to null. (No FirePoint)"); 
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(fireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        } 
        else
        {
            if( Input.GetKey(KeyCode.Space) && Time.time > timeToFire)
            {
                timeToFire = Time.time + (1 / fireRate);
                Fire();
            }
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        Instantiate( bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
