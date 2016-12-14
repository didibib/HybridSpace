using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BreakerController : MonoBehaviour {

    public GameObject[] breakers;
    Toggle[] toggles;
    public float[] breakervalues;
    public float maxload;
    bool overloaded, resetting;
    float resettimer;

    private void Start()
    {
        toggles = GetComponentsInChildren<Toggle>();
    }

    void Update()
    {
        float currentload = 0;
        for(int i = 0; i < breakers.Length; i++)
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
        if(resetting)
        {
            resettimer -= Time.deltaTime;
            if(resettimer <= 0)
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
            resettimer = 3;
            for(int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = false;
                toggles[i].isOn = false;
            }
        }
    }
}
