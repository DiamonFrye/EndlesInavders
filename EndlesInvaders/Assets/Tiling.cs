using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int CameraGroundOffsetY = 2;
    public float offset = 3f;
    public float distancePerFrame = 0.001f;
    public bool hasATopBuddy = false;
    public bool hasABottomBuddy = false;
    // used if the object is not tilable
    public bool reverseScale = false;

    private float spriteHeight = 0f;
    private Camera cam;
    private Transform myTransform;
    private float camVerticalExtend = 0f;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteHeight = sRenderer.sprite.bounds.size.y;
        camVerticalExtend = cam.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        // does is still need buddies? if not do nothing
		if(hasABottomBuddy == false || hasATopBuddy == false)
        {
            // calculate the cameras extend (half the height) of the camera can see in world coordinates
            camVerticalExtend = cam.orthographicSize;

            // calculate the y pos where camera can se the edge of the sprite
            float edgeVisiblePosTop = (myTransform.position.y + spriteHeight / 2) - camVerticalExtend;
            float edgeVisiblePosBottom = (myTransform.position.y - spriteHeight / 2) - camVerticalExtend;

            // checking if we can see the edge of the element and then calling MakeNewBuddy if we can
            if (cam.transform.position.y >= edgeVisiblePosTop - CameraGroundOffsetY && hasATopBuddy == false)
            {
                MakeNewBuddy(1);
                hasATopBuddy = true;
            }
            else if (cam.transform.position.y <= edgeVisiblePosBottom + CameraGroundOffsetY && hasABottomBuddy == false)
            {
                MakeNewBuddy(-1);
                hasABottomBuddy = true;
            }
        }
        MoveBackground();
    }

    // function that create new buddy no the side required
    void MakeNewBuddy (int topOrBottom)
    {
        //calculating the new position for new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + spriteHeight * topOrBottom * Mathf.Abs(transform.localScale.y) - 0.01f, myTransform.position.z);

        // instanting our new buddy and storing him in new variable
        Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;
         
        // if not tilable let's reverse the y size
        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x, newBuddy.localScale.y *-1, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
         
        if (topOrBottom > 0)
        {
            newBuddy.GetComponent<Tiling>().hasABottomBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasATopBuddy = true;
        }
        
    }

    void MoveBackground()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 newPosition = new Vector3(position.x, position.y - distancePerFrame, position.z);
        transform.position = newPosition;

        if (newPosition.y <= -(2 * camVerticalExtend + offset))
        {
            Destroy(transform.gameObject);
        }
    }
}
