using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSchematic : MonoBehaviour
{
    public GameObject[] schematics;
    public GameObject errorText;

    void Start()
    {
        for (int i = 0; i < schematics.Length; i++)
        {
            schematics[i].SetActive(false);
        }
        errorText.SetActive(false);
    }

    void OnGUI()
    {
        if (errorText.activeSelf)
        {
            float blink = Mathf.Sin(Time.time * 3);
            errorText.GetComponent<Text>().color = new Color(0, 0, 0, blink);
        }
       
    }

    public void TextChanged(string newText)
    {
        int number;
        string text;
        if (int.TryParse(newText, out number))
        {
            if (number >= 0 && number < schematics.Length)
            {
                errorText.SetActive(false);
                ViewSchematic(number);
            } else
            {
                Error();
            }
        }
        else if (newText == "cam")
        {
            errorText.SetActive(false);

            // hier roep je ToggleScreen() aan

            Debug.Log("Switch to camera");
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
            schematics[i].SetActive(false);
        }
        schematics[level].SetActive(true);
    }

    void Error()
    {
        for (int i = 0; i < schematics.Length; i++)
        {
            schematics[i].SetActive(false);
        }
        errorText.SetActive(true);
    }
}
