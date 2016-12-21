using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryList : MonoBehaviour {

    Canvas canvas;
    public GameObject panel;

    float setScaleX, setScaleY;
    List<GameObject> inventory = new List<GameObject>();
    public List<Image> slots = new List<Image>();

	void Start () {
        canvas = transform.GetComponent<Canvas>();
        Vector3 scale = canvas.GetComponent<RectTransform>().lossyScale;
        setScaleX = 1 / scale.x;
        setScaleY = 1 / scale.y;
	}
	
	void Update () {
	    
	}

    void PutInInventory(GameObject _object)
    {
        
    }
}
