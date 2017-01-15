using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSchematic : MonoBehaviour
{
    public GameObject[] schematics;
    public GameObject errorText;
    public GameObject infoText;

    //public GameObject breakerbox;// breakerbox to power the elevator
    public GameObject noPowerText; // if elevator has no power
    public GameObject callingText; // calling elevator

    void Start()
    {
        for (int i = 0; i < schematics.Length; i++)
        {
            if (schematics[i] != null)
                schematics[i].SetActive(false);
        }
        errorText.SetActive(false);
        infoText.SetActive(true);
        noPowerText.SetActive(false);
    }

    void OnGUI()
    {
        if (errorText.activeSelf)
        {
            float blink = Mathf.Sin(Time.time * 3) + .5f;
            errorText.GetComponent<Text>().color = new Color(0, 0, 0, blink);
        }
        else if (noPowerText.activeSelf)
        {
            float blink = Mathf.Sin(Time.time * 3) + .5f;
            noPowerText.GetComponent<Text>().color = new Color(0, 0, 0, blink);
        }
        else if (callingText.activeSelf)
        {
            float blink = Mathf.Sin(Time.time * 3) + .5f;
            callingText.GetComponent<Text>().color = new Color(0, 0, 0, blink);
        }

    }

    public void TextChanged(string newText)
    {
        errorText.SetActive(false);
        infoText.SetActive(false);
        noPowerText.SetActive(false);

        int number;
        if (int.TryParse(newText, out number))
        {
            if (schematics[number] != null)
            {
                if (number >= 0 && number < schematics.Length)
                {
                    ViewSchematic(number);
                }
            }
            else
            {
                Error();
            }
        }
        else if (newText == "cam")
        {
            // hier roep je ToggleScreen() aan

            Debug.Log("Switch to camera");
        }
        else if (newText == "info")
        {
            infoText.SetActive(true);
        }
        else if (newText == "elevator")
        {
            if (true/*breakerbox.Power()*/) // bool om te checken of de power aan is.
            {
                callingText.SetActive(true);
            }
            else
            {
                noPowerText.SetActive(true);
            }
        }
        else
        {
            Error();
        }
    }

    void ViewSchematic(int level)
    {
        for (int i = 0; i < schematics.Length; i++)
        {
            if (schematics[i] != null)
                schematics[i].SetActive(false);
        }
        schematics[level].SetActive(true);
    }

    void Error()
    {
        for (int i = 0; i < schematics.Length; i++)
        {
            if (schematics[i] != null)
                schematics[i].SetActive(false);
        }
        errorText.SetActive(true);
    }
}
