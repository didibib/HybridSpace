using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour
{
    Rigidbody thisRigidbody;
    [Range(.1f, .5f)]
    public float setMiniScale = 0.5f;

    Vector3 miniScale;
    bool isGrabbed = false;

    void Start()
    {
        thisRigidbody = transform.GetComponent<Rigidbody>();
        miniScale = new Vector3(setMiniScale, setMiniScale, setMiniScale);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Slot")
        {
            transform.localScale = miniScale;

            if (!isGrabbed)
            {
                transform.SetParent(col.transform);

                transform.position = col.transform.position;
                transform.rotation = col.transform.rotation;

                thisRigidbody.useGravity = false;
                thisRigidbody.isKinematic = true;
            }            
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Slot" && !isGrabbed)
        {
            transform.SetParent(col.transform);
            //transform.localScale = miniScale;

            transform.position = col.transform.position;
            transform.rotation = col.transform.rotation;

            thisRigidbody.useGravity = false;
            thisRigidbody.isKinematic = true;
        }
        else if (isGrabbed)
        {
            transform.parent = null;
        }
    }

    void OnTriggerExit(Collider col)
    {
        transform.parent = null;

        thisRigidbody.useGravity = true;
        thisRigidbody.isKinematic = false;

        transform.localScale = new Vector3(1, 1, 1);
    }

    void OnGrabbed(bool _isGrabbed)
    {
        isGrabbed = _isGrabbed;
    }
}