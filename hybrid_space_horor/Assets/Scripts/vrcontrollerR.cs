using UnityEngine;
using System.Collections;

public class vrcontrollerR : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;
    public bool touchPadDown = false;
    public bool touchPadUp = false;
    public bool touchPadPressed = false;

    //Game Variables
    public GameObject Flashlight;
    public Battery light;
    GameObject Monster;

	// Use this for initialization
	void Start () {
	    trackedObj = GetComponent<SteamVR_TrackedObject>();
        Monster = GameObject.FindGameObjectWithTag("Monster");
	}
	
	// Update is called once per frame
	void Update () {
        if(controller == null)
        {
            Debug.Log("Controller is not initialized");
            return;
        }

	    gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);
        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);
        touchPadDown = controller.GetPressDown(touchPad);
        touchPadUp = controller.GetPressUp(touchPad);
        touchPadPressed = controller.GetPress(touchPad);

        if (touchPadUp)
        {
            Debug.Log("touchPad is Up");
        }
        if (touchPadDown)
        {
            Debug.Log("touchPad is Down");
        }
        if (triggerButtonPressed)
        {
            Debug.Log("triggerButton is pressed");
            light.on = true;
        }
        else
        {
            light.on = false;
        }
        if (triggerButtonUp)
        {
            Debug.Log("triggerButton is Up");
        }
        if(triggerButtonDown)
        {
            Debug.Log("triggerButton is Down");
        }

        Flashlight.transform.position = transform.position;
        Flashlight.transform.forward = transform.forward;

        if(CheckCollision(Flashlight.transform, Monster.transform, Flashlight.GetComponent<Light>(), "Monster"))
        {
            Monster.GetComponent<MonsterController>().EnterLight(Flashlight.transform.position, Monster.transform.position);
        }
	}

    public bool CheckCollision(Transform origin, Transform target, Light light, string targettag)
    {
        Vector3 dist = target.position - origin.position;
        Vector3 dir = dist.normalized;
        float angle = Vector3.Angle(origin.forward, dir);
        if (angle > light.spotAngle / 2)
            return false;
        else
        {
            Ray ray = new Ray(origin.position, dir);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, light.range))
            {
                if (hit.collider.gameObject.tag == targettag)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
