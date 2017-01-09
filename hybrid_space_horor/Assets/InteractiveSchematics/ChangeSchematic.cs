using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSchematic : MonoBehaviour
{
    public GameObject[] schematics;
    public GameObject errorText;
    public GameObject infoText;

    void Start()
    {
        for (int i = 0; i < schematics.Length; i++)
        {
            schematics[i].SetActive(false);
        }
        errorText.SetActive(false);
        infoText.SetActive(true);
    }

    void OnGUI()
    {
        if (errorText.activeSelf)
        {
            float blink = Mathf.Sin(Time.time * 3) + .5f;
            errorText.GetComponent<Text>().color = new Color(0, 0, 0, blink);
        }
    }

    public void TextChanged(string newText)
    {
        errorText.SetActive(false);
        infoText.SetActive(false);

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
