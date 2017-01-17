using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour
{
    private float fillAmount;
    private float currentAmount;
    public float speed;

    public Color full, empty;
    float increment;
    private Image image;

    void Start()
    {
        fillAmount = 1;
        image =  transform.GetComponent<Image>();
        image.color = full;
    }

    void Update()
    {
        currentAmount = image.fillAmount;
        if (Input.GetMouseButton(2))
        {
            image.fillAmount -= Time.deltaTime / speed; 
            increment += Time.deltaTime / speed;
        }
        if(image.fillAmount >= 0) image.color = Color.Lerp(full, empty, increment);
    }

    public void AddEnergy(float energy)
    {
        image.fillAmount += energy;
        if(image.fillAmount == 1)
        {
            image.color = full;
        }
        increment *= energy;
    }
}
