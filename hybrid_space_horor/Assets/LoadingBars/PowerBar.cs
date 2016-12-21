using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerBar : MonoBehaviour {

    public int maxUsage;

    public Transform loadingBar;

    [HideInInspector]
    private float maxAmount;   
    [HideInInspector]
    private float currentAmount;
    [SerializeField]
    private float speed;

    void Start()
    {
        maxAmount = 1;
        currentAmount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputPower(20);
        }

        if (currentAmount < maxAmount)
        {
            currentAmount += speed * Time.deltaTime;
        }
        else if(maxAmount >= maxUsage)
        {
            // put here the code to execute when the the bar is full
        }
        loadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
    }

    void InputPower(float _power)
    {
        maxAmount += _power;
    }
}
