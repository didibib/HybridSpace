using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{

    public int cameraCooldownTime;
    public int maxAmount;

    public Transform loadingBar;
    public Transform textLoading;

    public Text textRecharging; // in plaats van text moet er een texture gebruikt worden, want anders verdwijnt het achter de fading;
    [HideInInspector]
    private Color textColor;
    private bool inCooldown;

    [HideInInspector]
    private float currentAmount;
    [SerializeField]
    private float speed;

    void Start()
    {
        textRecharging.color = new Color(1, 0, 0, 0);
        inCooldown = false;
        currentAmount = maxAmount;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentAmount = maxAmount;
        }

        if (currentAmount >= 0)
        {
            currentAmount -= speed * Time.deltaTime;
            textLoading.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
        }
        else
        {
            textLoading.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
            StartCoroutine(cameraCooldown());

        }
        loadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;

        if (inCooldown)
        {
            float blink = Mathf.Sin(Time.time * 3);
            textRecharging.color = new Color(1, 0, 0, blink);
        } else
        {
            textRecharging.color = new Color(1, 0, 0, 0);
        }
    }

    void OnGUI()
    {
        if (inCooldown)
        {
            float blink = Mathf.Sin(Time.time * 3);
            textRecharging.color = new Color(1, 0, 0, blink);
        }
        else
        {
            textRecharging.color = new Color(1, 0, 0, 0);
        }
    }

    IEnumerator cameraCooldown()
    {
        float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
        inCooldown = true;
        yield return new WaitForSeconds(fadeTime);

        yield return new WaitForSeconds(cameraCooldownTime);

        inCooldown = false;
        GameObject.Find("_GM").GetComponent<Fading>().BeginFade(-1);
        currentAmount = maxAmount;
    }
}
