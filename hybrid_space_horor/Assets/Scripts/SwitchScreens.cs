using UnityEngine;
using System.Collections;

public class SwitchScreens : MonoBehaviour {

    public Canvas canvas;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            canvas.GetComponent<Canvas>().enabled = !canvas.GetComponent<Canvas>().enabled;
        }
	}
}
