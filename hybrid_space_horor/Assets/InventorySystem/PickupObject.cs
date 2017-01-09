using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour
{
    Rigidbody thisRigidbody;

    [Range(.1f, .5f)]
    public float setMiniScale = 0.5f;
    Vector3 miniScale;

    Vector3 localScale;

    bool isGrabbed = false;

    public string interactWithName;
    public GameObject interactWithObject;

    void Start()
    {
        thisRigidbody = transform.GetComponent<Rigidbody>();

        localScale = transform.lossyScale;
        miniScale = new Vector3(localScale.x * setMiniScale / 1, localScale.y * setMiniScale / 1, localScale.z * setMiniScale / 1);
        
        // interactWithObject.NVRInteractableItem.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Slot")
        {
            transform.localScale = miniScale;
            Debug.Log("trigger enter " + transform.localScale);

            if (!isGrabbed)
            {
                transform.SetParent(col.transform);

                transform.position = col.transform.position;
                transform.rotation = col.transform.rotation;

                thisRigidbody.useGravity = false;
                thisRigidbody.isKinematic = true;
            }            
        }

        if (col.name == interactWithName)
        {
            Debug.Log("interacted");
            // interactWithObject.NVRInteractableItem.enabled = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Slot" && !isGrabbed)
        {
            transform.SetParent(col.transform);

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

        transform.localScale = localScale;
    }

    void OnGrabbed(bool _isGrabbed)
    {
        isGrabbed = _isGrabbed;
    }
}