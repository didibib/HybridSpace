using UnityEngine;
using System.Collections;

public class PlayerSmokeController : MonoBehaviour {
    GameObject Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Player.transform.position + new Vector3(0, 1, 0);
	}
}
