using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

    public GameObject ending;
    public float endTime;
    
	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Victory")
        {
            // stop player movement;

            StartCoroutine(Ending());
            Debug.Log(Time.deltaTime);
        }
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(endTime);
        ending.GetComponent<Fading>().BeginFade(1);        
    }
}