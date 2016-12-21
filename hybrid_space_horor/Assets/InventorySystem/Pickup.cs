using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pickup : MonoBehaviour
{

    public Camera cam;
    public Canvas invCanvas;
    public LayerMask pickupItemLayer;
    public LayerMask slotLayer;

    [HideInInspector]
    public Vector3 posInvCanvas;
    [Range(0, 5)]
    public int distanceCanvas;
    public int rayLength;

    [HideInInspector]
    public int click = 0;

    Transform target;

    void Start()
    {
        invCanvas.GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        posInvCanvas = cam.transform.position + cam.transform.forward * distanceCanvas;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength, pickupItemLayer))
            {
                target = hit.transform;
                target.GetComponent<Rigidbody>().useGravity = false;

                target.rotation = cam.transform.rotation;
                target.position = ray.origin + ray.direction * 3;
            }

            if (Physics.Raycast(ray, out hit, rayLength, slotLayer))
            {
                Image slot = hit.transform.GetComponent<Image>();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength, slotLayer))
            {
                Transform slotPos = hit.transform;
                target.transform.SetParent(slotPos, true);
                target.position = slotPos.position;
            } else
            {
                target.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            invCanvas.transform.position = posInvCanvas;
            invCanvas.transform.rotation = cam.transform.rotation;
            invCanvas.GetComponent<Canvas>().enabled = !invCanvas.GetComponent<Canvas>().enabled;
        }
    }
}
