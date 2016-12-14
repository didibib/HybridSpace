using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleColor : MonoBehaviour {

    public Color offColor, onColor, disabledColor;
    Toggle t;
    Image i;

	// Use this for initialization
	void Start ()
    {
        t = GetComponent<Toggle>();
        i = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (t.interactable)
        {
            if (t.isOn)
                i.color = onColor;
            else
                i.color = offColor;
        }
        else
            i.color = disabledColor;
    }
}
