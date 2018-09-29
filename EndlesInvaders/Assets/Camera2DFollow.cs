using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float speed = 0.001f;
        private bool searchingForPlayer = false;
        private bool gamePause = false;
//        float camHorizontalExtend = 0f;
//        float camVerticalExtend = 0f;
 //       float offset = 2f;

        // Update is called once per frame
        private void Start()
        {
            if(target == null)
            {
                gamePause = true;
                if (!searchingForPlayer)
                {
                    searchingForPlayer = true;
                    StartCoroutine(SearchForPlayer());
                }
                return;
            }
//            camVerticalExtend = Camera.main.orthographicSize ;
//            camHorizontalExtend = Camera.main.orthographicSize * Screen.width / Screen.height ;
            // TODO: Play game

        }
         
        IEnumerator SearchForPlayer()
        {
            GameObject sResult = GameObject.FindGameObjectWithTag("Player");
            if (sResult == null)
            {
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(SearchForPlayer());
            }
            else
            {
                searchingForPlayer = false;
                target = sResult.transform;
                gamePause = false;
                yield return false;
            }
        }

        private void FixedUpdate()
        {
            StartCoroutine(fUpdate());
        }

        IEnumerator fUpdate()
        {
            if (gamePause == true)
            {
                yield return null;
            }
             
            Vector3 cameraPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            Vector3 newPosition = new Vector3(cameraPosition.x, cameraPosition.y + speed, cameraPosition.z);
            transform.position = newPosition;

        }
    }
}
