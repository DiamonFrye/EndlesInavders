using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        public int Health = 100;
    }

    public EnemyStats enemyStats = new EnemyStats();
    public Transform destroyEffectPrefab;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () { 
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageEnemy(int damageAmount)
    {
        enemyStats.Health -= damageAmount;
        if (enemyStats.Health <= 0)
        {
            Transform clone = Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
            Destroy(clone.gameObject, .5f);
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 oldVelocity = rb.velocity;
        if (collision.gameObject.tag.Equals("PlayerBullet"))
        {
            Destroy(collision.gameObject);
        }
    }

}
