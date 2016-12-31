using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BreakerController : MonoBehaviour
{

    public GameObject[] breakers;
    Toggle[] toggles;
    public float[] breakervalues;
    public float maxload, maxreset, maxcamerabattery;
    [HideInInspector]
    public bool overloaded, resetting;
    public GameObject lightbuttons, tvscreen, camerabatterybar;
    [HideInInspector]
    public float currentload, resettimer, currentbattery;

    private void Start()
    {
        toggles = GetComponentsInChildren<Toggle>();
        currentbattery = maxcamerabattery;
    }

    void Update()
    {
        currentload = 0;
        for (int i = 0; i < breakers.Length; i++)
        {
            if (breakers[i].activeInHierarchy)
                currentload += breakervalues[i];
        }
        if (currentload >= maxload)
        {
            overloaded = true;
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = false;
                toggles[i].isOn = false;
            }
        }

        if (overloaded || resetting)
        {
            for (int i = 0; i < breakers.Length; i++)
                breakers[i].SetActive(false);
        }
        if (resetting)
        {
            resettimer -= Time.deltaTime;
            if (resettimer <= 0)
            {
                resetting = false;
                overloaded = false;
                for (int i = 0; i < toggles.Length; i++)
                {
                    toggles[i].interactable = true;
                    toggles[i].isOn = false;
                }
            }
        }

        if (!lightbuttons.activeInHierarchy)
        {
            currentbattery -= Time.deltaTime;
            if (currentbattery < 0)
                tvscreen.SetActive(false);
        }
    }

    public void ToggleBreaker(int index)
    {
        breakers[index].SetActive(!breakers[index].activeInHierarchy);
    }

    public void Reset()
    {
        if (!resetting)
        {
            resetting = true;
            resettimer = maxreset;
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = false;
                toggles[i].isOn = false;
            }
        }
    }

    public void ToggleScreen()
    {
        if (lightbuttons.activeInHierarchy)
        {
            lightbuttons.SetActive(false);
            camerabatterybar.SetActive(true);
            if (currentbattery > 0)
                tvscreen.SetActive(true);
        }
        else
        {
            lightbuttons.SetActive(true);
            tvscreen.SetActive(false);
            camerabatterybar.SetActive(false);
        }
    }
}
