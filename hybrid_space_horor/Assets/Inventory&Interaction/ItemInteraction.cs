using UnityEngine;
using System.Collections;

public class ItemInteraction : PickupObject
{
    public string interactWithName; // the name in inspector
    public GameObject interactWithObject;
        // Voorbeeld: met een keycard wil je een deur unlocken. 
        // interactWithName = dan de keycard reader.
        // interactWithObject = dan de deur.

    // is this object a keycard or battery
    public bool isKeycard; // keycardreaders moeten een audiosource hebben
    public bool isBattery; // de zaklamp moet een audiosource hebben
    bool isNVR;

    // audio for the keycardreader
    public AudioClip accessGrantedAudio;
    public AudioClip accessDeniedAudio;
    public AudioClip insertBatteryAudio;

    public override void Start()
    {
        base.Start();
        if (interactWithObject.GetComponent<NVRInteractableItem>() != null)
        {
            isNVR = true;
            interactWithObject.GetComponent<NVRInteractableItem>().enabled = false;
        }
        else
        {
            isNVR = false;
            Debug.Log("This object does not use Newton VR");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        float audioPitch = Random.Range(0.95f, 1.05f); ;
        AudioSource audioSource = col.GetComponent<AudioSource>();
        if (col.tag == "KeycardReader" && isKeycard)
        {
            if (col.name == interactWithName) // check if this is the keycard is meant for this reader
            {
                if (isNVR)
                {
                    audioSource.clip = accessGrantedAudio;
                    audioSource.pitch = audioPitch;
                    audioSource.Play();
                    interactWithObject.GetComponent<NVRInteractableItem>().enabled = true;
                }
            }
            else
            {
                audioSource.clip = accessDeniedAudio;
                audioSource.pitch = audioPitch;
                audioSource.Play();
            }
        }
        else if (col.tag == "Battery" && isBattery)
        {
            audioSource.clip = insertBatteryAudio;
            audioSource.pitch = audioPitch;
            audioSource.Play();

            // add to battery

            Object.Destroy(gameObject);
        }
    }
}
