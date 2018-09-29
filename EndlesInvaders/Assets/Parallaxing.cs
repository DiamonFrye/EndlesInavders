
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;     // Array (list) of all the back - and forfgrounds to be parallaxed
    private float[] parallaxScales;     // The proporcion of the camera's move the background by
    public float smoothing = 1;         // Make sure to set above 0

    private Transform cam;              // References to the main camera transform
    private Vector3 previousCamPos;     // Store camera position in the previous frame

    // Is called before Start(). Great for references.
    void Awake()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start () {
        // The previous frame had cuttent frame camera position
        previousCamPos = cam.position;

        // asigning coresponding parallaxScale
        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {

        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the oposite of the camera movment because the previouse frame multiplied by the scale
            float parallaxX = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            // set a target x position witch is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY;

            // crate a target position witch is the background current posiotion with it's target x position
            Vector3 bacgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

            // fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bacgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
