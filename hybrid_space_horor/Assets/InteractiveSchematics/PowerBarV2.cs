using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerBarV2 : MonoBehaviour
{

    public BreakerController breakers;
    public Image bar;
    public Image overloadImg;
    public Color overloadColor;
    public GameObject overloadTxt;

    void Start()
    {
        overloadImg.GetComponent<Image>().color = new Color(241, 241, 241);
        overloadTxt.SetActive(false);
    }

    void Update()
    {
        if (!breakers.overloaded && !breakers.resetting)
        {
            overloadImg.GetComponent<Image>().color = new Color(241, 241, 241);
            float percentage = (breakers.currentload / breakers.maxload);
            bar.fillAmount = percentage;
        }
        else if (breakers.resetting)
        {
            overloadTxt.SetActive(false);
            float percentage = Mathf.Round(breakers.resettimer / breakers.maxreset * 10f) / 10f;
            Debug.Log(percentage);
            bar.fillAmount = percentage;
        }
        else
        {
            overloadTxt.SetActive(true);
            float blink = Mathf.Sin(Time.time * 3) + .5f;
            //overloadTxt.GetComponent<Text>().color = new Color(-blink, 0, 0);
            overloadImg.GetComponent<Image>().color = new Color(overloadColor.r, overloadColor.g, overloadColor.b, blink);
            if (bar.fillAmount > 0)
            {
                bar.fillAmount -= 0.4f * Time.deltaTime;
            }
        }
    }

}
