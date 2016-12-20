using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectHit : MonoBehaviour {

    void Update() {
        if (HitByRay())
        {
            transform.GetComponent<Image>().color = Color.red;
        } else
        {
            transform.GetComponent<Image>().color = Color.white;
        }
    }

	bool HitByRay()
    {
        return true;
    }
}
