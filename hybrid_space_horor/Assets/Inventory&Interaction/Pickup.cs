using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public Camera cam;
    public Canvas invCanvas;
    bool showCanvas = false;

    public LayerMask pickupLayer;

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
        if (invCanvas != null)
            invCanvas.gameObject.SetActive(showCanvas);
    }

    void Update()
    {
        if (invCanvas != null)
        {
            posInvCanvas = cam.transform.position + cam.transform.forward * distanceCanvas;
            invCanvas.gameObject.SetActive(showCanvas);
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength, pickupLayer))
            {
                target = hit.transform;
                target.SendMessage("OnGrabbed", true);
                target.GetComponent<Rigidbody>().useGravity = false;
                target.rotation = cam.transform.rotation;
                target.position = ray.origin + ray.direction * 2;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (target != null)
            {
                target.GetComponent<Rigidbody>().useGravity = true;
                target.SendMessage("OnGrabbed", false);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            invCanvas.transform.position = posInvCanvas;
            invCanvas.transform.rotation = cam.transform.rotation;
            showCanvas = !showCanvas;
        }
    }
}
