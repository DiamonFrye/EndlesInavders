using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float dumbTime = 0f;
    public float movePower = 2f;

    private Rigidbody2D rb;
    private float nextTimeToMove = 0;
    private float camHorizontalExtend, camVerticalExtend;
    private bool onTheBattlefield = false;
    private float battelfieldRedyPosition = 0f;
//    private int cnt = 0;                                    // used to find bugs in movment ( especially first position on battelfield)

    public bool leftCollision = false;
    public bool rightCollision = false;
    private Transform childGameObject;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        childGameObject = transform.Find("Weapon");
        if(childGameObject == null)
        {
            Debug.LogError("No weapon find - " + transform.name);
        }
        camVerticalExtend = Camera.main.orthographicSize;
        camHorizontalExtend = Camera.main.orthographicSize * Screen.width / Screen.height;
        battelfieldRedyPosition = Random.Range(2f, camVerticalExtend - 0.5f);
        EngageToGame();
    }
	
	// Update is called once per frame
	void Update () {
        if(onTheBattlefield == true)
        {
            CollisionChceck();
            BorderCheack();
        }
        else
        {
            OnTheBattelfieldCheck();
        }
    }

    private void OnTheBattelfieldCheck()
    {
        Vector2 myPosition = transform.position;
 //       Debug.Log("Position : " + myPosition + " Needed " + battelfieldRedyPosition);
        EngageToGame();
        if (myPosition.y <= battelfieldRedyPosition)
        {
            onTheBattlefield = true;
            childGameObject.GetComponent<EnemyWeapon>().firePremission = true;
        }
    }

    private void EngageToGame()
    {
        float randomPower = Random.Range(2, 3);
        rb.velocity = new Vector2(0, -randomPower);
    }

    private void BorderCheack()
    {
        if (transform.position.x <= -camHorizontalExtend + 1f)
        {
            Move(movePower * 3, 1);
            nextTimeToMove = Time.time + dumbTime;
        }
        else if (transform.position.x >= camHorizontalExtend - 1f)
        {
            Move(movePower * 3, 2);
            nextTimeToMove = Time.time + dumbTime;
        }
        else if (transform.position.y <= 0f)
        {
            Move(movePower * 1, 3);
            nextTimeToMove = Time.time + dumbTime;
        }
        else if (transform.position.y >= camVerticalExtend - 1f)
        {
            Move(movePower * 1, 4);
            nextTimeToMove = Time.time + dumbTime;
        }
        else if (nextTimeToMove <= Time.time)
        {
            Move(movePower, 0);
            nextTimeToMove = Time.time + dumbTime;
        }
    }

    private void CollisionChceck()
    {
        if (rightCollision == true && leftCollision == true)
        {
            Move(0, 2);
            nextTimeToMove = Time.time + dumbTime;
            rightCollision = false;
            leftCollision = false;
        }
        if (leftCollision == true)
        {
            Move(movePower, 1);
            nextTimeToMove = Time.time + dumbTime;
            leftCollision = false;
        }
        if (rightCollision == true)
        {
            Move(movePower, 2);
            nextTimeToMove = Time.time + dumbTime;
            rightCollision = false;
        }
    }

    private void Move(float power, int special)
    {
        float randomPowerX = Random.Range(0, power);
        float randomPowerY = Random.Range(0, power);
        float randomDirectionX = Random.Range(0f, 1f);
        float randomDirectionY = Random.Range(0f, 1f);
        if (special == 1)                                // Move right
        {
            rb.velocity = new Vector2(randomPowerX, 0);
        }
        else if (special == 2)                          // Move left
        {
            rb.velocity = new Vector2(-randomPowerX, 0);
        }
        else if (special == 3)                          // Move up
        {
            rb.velocity = new Vector2(0, randomPowerY);
        }
        else if (special == 4)                          // Move down
        {
            rb.velocity = new Vector2(0, -randomPowerY);
        }
        else if (randomDirectionX >= 0.5f)
        {
            if (randomDirectionY >= 0.5f)
            {
                rb.velocity = new Vector2(randomPowerX, randomPowerY);
            }
            else
            {
                rb.velocity = new Vector2(randomPowerX, -randomPowerY);
            }
        }
        else
        {
            if (randomDirectionY >= 0.5f)
            {
                rb.velocity = new Vector2(-randomPowerX, randomPowerY);
            }
            else
            {
                rb.velocity = new Vector2(-randomPowerX, -randomPowerY);
            }
        }
//        Debug.Log("Random = " + randomPower + " Direction: " + randomDirection);
    }
}
