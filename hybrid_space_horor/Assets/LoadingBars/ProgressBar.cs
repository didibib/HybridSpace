using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public int maxAmount;

    public Transform loadingBar;
    public Transform textLoading;
    [HideInInspector]
    private float currentAmount;
    [SerializeField]
    private float speed;	

    void Start()
    {
        currentAmount = maxAmount;
    }

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            currentAmount = maxAmount;
        }

        if (currentAmount >= 0)
        {
            currentAmount -= speed * Time.deltaTime;
            textLoading.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
        } else
        {
            textLoading.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
            // put here the code to execute when the the bar is full
        }
        loadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
