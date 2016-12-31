using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleColor : MonoBehaviour {

    public Sprite offImage, onImage;
    public Color disabledColor;
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
                i.sprite = onImage;
            else
                i.sprite = offImage;
        }
        else
            i.color = disabledColor;
    }
}
