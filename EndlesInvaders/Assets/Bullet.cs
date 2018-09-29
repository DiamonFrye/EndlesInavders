using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float velY = 5f;
    public float velX = 0f;
    public int damage = 1; 
    public bool playerBullet = false;
    public Transform destroyEffectPrefab;

    private int cnt;
    private Rigidbody2D rb;
    private float camVerticalExtend;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        camVerticalExtend = Camera.main.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(velX, velY);
        PositionDestroy(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerBullet == true)
        {

            if (collision.gameObject.tag.Equals("EnemyBullet"))
            {
                //TODO: ? some bullets are better (stronger) and maybe they are not destroyed?
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag.Equals("Enemy"))
            {
                Transform clone = Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
                Destroy(clone.gameObject, .5f);
                collision.gameObject.GetComponent<Enemy>().DamageEnemy(damage); 
            }
        }
        else
        {
            if (collision.gameObject.tag.Equals("PlayerBullet"))
            {
                //TODO: ? some bullets are better (stronger) and maybe they are not destroyed?
                Transform clone = Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
                Destroy(clone.gameObject, .5f); 
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag.Equals("Player"))
            {
                Transform clone = Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
                Destroy(clone.gameObject, .5f);
                collision.gameObject.GetComponent<Player>().DamagePlayer(damage);
            }
        }
    }

    private void PositionDestroy()
    {
        float myPosition = transform.position.y;
        if(myPosition <= -camVerticalExtend - 0f || myPosition >= camVerticalExtend + 0f)
        {
            Destroy(transform.gameObject);
        }
    }
}
