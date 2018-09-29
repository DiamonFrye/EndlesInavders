using UnityEngine;

public class Player : MonoBehaviour {
    
    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
    }

    public PlayerStats playerStats = new PlayerStats(); 

    private void Update()
    {

    }

    public void DamagePlayer(int damageAmount)
    {
        playerStats.Health -= damageAmount;
        if(playerStats.Health <= 0)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("EnemyBullet"))
        {
            Destroy(collision.gameObject);
        }

    }
}
