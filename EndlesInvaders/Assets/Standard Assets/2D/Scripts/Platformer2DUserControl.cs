using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private float camHorizontalExtend = 0f;
        private float camVerticalExtend = 0f;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Start()
        {
            camVerticalExtend = Camera.main.orthographicSize;
            camHorizontalExtend = Camera.main.orthographicSize * Screen.width / Screen.height;
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            // Pass all parameters to the character control script.
            if(m_Character.leftEnable == true && m_Character.rightEnable == true)
            {
                m_Character.Move(h, false);
            }
            else if(m_Character.leftEnable == false && m_Character.rightEnable == true)
            {
                if (h > 0f)
                {
                    m_Character.Move(h, false);
                }
            }
            else
            {
                if (h < 0f)
                {
                    m_Character.Move(h, false);
                }
            }

            if (m_Character.downEnable == true && m_Character.upEnable == true)
            {
                m_Character.Move(v, true);
            }
            else if (m_Character.downEnable == false && m_Character.upEnable == true)
            {
                if (v > 0f)
                {
//                    Debug.Log("sadasd");
                    m_Character.Move(v, true);
                }
            }
            else
            {
                if (v < 0f)
                {
                    m_Character.Move(v, true);
                }
            }
        }
    }
}
