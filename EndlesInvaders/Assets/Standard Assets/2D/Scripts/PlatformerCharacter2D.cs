using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;                    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f;                 // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;                            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;                   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f;                 // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;                  // For determining which way the player is currently facing.
        private float camHorizontalExtend = 0f;
        private float camVerticalExtend = 0f;

        public bool leftEnable = true, rightEnable = true, 
            upEnable = true, downEnable = true;             // Enable movment directions

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            camVerticalExtend = Camera.main.orthographicSize;
            camHorizontalExtend = Camera.main.orthographicSize * Screen.width / Screen.height;
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            CheckMovmentDierection();
 //           Vector3 newPosition = new Vector3(Mathf.Clamp(transform.position.x, -camHorizontalExtend, camHorizontalExtend),
//                           Mathf.Clamp(transform.position.y, -camVerticalExtend, 1f), transform.position.z);
//            transform.position = newPosition;

        } 

        private void CheckMovmentDierection()
        {
            if (transform.position.x > -camHorizontalExtend + 0.1f)
                leftEnable = true;
            if (transform.position.x < camHorizontalExtend - 0.1f)
                rightEnable = true;
            if (transform.position.y < 1f - 0.1f)
                upEnable = true;
            if (transform.position.y > -camVerticalExtend + 0.1f)
                downEnable = true;
            if (transform.position.x >= camHorizontalExtend - 0.1f && rightEnable == true)
            {
                rightEnable = false;
                m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
                Debug.Log("r");
            }
            else if (transform.position.x <= -camHorizontalExtend + 0.1f && leftEnable == true)
            {
                leftEnable = false;
                m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
                Debug.Log("l");
            }
            if (transform.position.y >= 1f - 0.1f && upEnable == true)
            {
                upEnable = false;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                Debug.Log("u");
            }
            else if (transform.position.y <= -camVerticalExtend + 0.1f && downEnable == true)
            {
                downEnable = false;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                Debug.Log("d");
            }
//            Debug.Log(rightEnable + " " + leftEnable + " " + upEnable + " " + downEnable);
        }

        public void Move(float move, bool top)
        {
            //only control the player if grounded or airControl is turned on
            if (m_AirControl)
            {
                // Move the character
                if (top == false)
                {
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                }
                else
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, move * m_MaxSpeed);
                }
            }
        }
    }
}
