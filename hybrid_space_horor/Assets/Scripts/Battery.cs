using UnityEngine;
using System;
using System.Collections;

public class Battery : MonoBehaviour {

    [HideInInspector]public bool on;
    public float batterytimeleft;
    Light light;
    public TextMesh text;

	// Use this for initialization
	void Start ()
    {
        on = false;
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = ((int)batterytimeleft).ToString();
        text.transform.rotation = transform.rotation;
        if (batterytimeleft < 20)
        {
            //Once the battery life is lower than 20 seconds, slowly lower the intensity of the light
            light.intensity = Math.Min((batterytimeleft / 20), 1);
        }

        if (on && batterytimeleft > 0)
        {
            //Only enable the light if the trigger is down and there is still battery life left
            batterytimeleft -= Time.deltaTime;
            light.enabled = true;
        }
        else
            light.enabled = false;
	}
}
