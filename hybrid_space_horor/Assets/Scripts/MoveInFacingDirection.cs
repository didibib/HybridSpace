using UnityEngine;
using System.Collections;
using Valve.VR;

public class MoveInFacingDirection : MonoBehaviour
{
    public GameObject player;

    public Camera camera;

    public float stepSize;

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    SteamVR_Controller.Device controller;
    SteamVR_TrackedObject trackedObject;

    Vector2 touchpad;

    private float sensitivityX = 1.5F;
    private Vector3 playerPos;

    void Start()
    {
        trackedObject = gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        controller = SteamVR_Controller.Input((int)controller.index);

        if (controller == null)
        {
            Debug.Log("Controller is not initialized");
            return;
        }

        if (controller.GetPressDown(touchPad))
        {
            // volgens mij zou een van de onderstaande dingen moeten werken
            player.transform.position += camera.transform.forward * stepSize;

            //player.transform.position += trackedObject.transform.forward* stepSize;
        }

        //If finger is on touchpad
        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Read the touchpad values
            touchpad = controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);


            // Handle movement via touchpad
            if (touchpad.y > 0.2f || touchpad.y < -0.2f)
            {
                // Move Forward
                player.transform.position -= player.transform.forward * Time.deltaTime * (touchpad.y * 5f);

                // Adjust height to terrain height at player positin
                playerPos = player.transform.position;
                playerPos.y = Terrain.activeTerrain.SampleHeight(player.transform.position);
                player.transform.position = playerPos;
            }

            // handle rotation via touchpad
            if (touchpad.x > 0.3f || touchpad.x < -0.3f)
            {
                player.transform.Rotate(0, touchpad.x * sensitivityX, 0);
            }

            //Debug.Log ("Touchpad X = " + touchpad.x + " : Touchpad Y = " + touchpad.y);
        }
    }
}