using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Vector3 spawnpoint;
    public GameObject playersmoke;
    [HideInInspector] public GameObject Monster;
    [HideInInspector] public GameObject Player;
    PlayerAnimationState state;
    public SteamVR_LaserPointer laserpointer;
    public SteamVR_Teleporter teleporter;
    public GameObject flashlight;

    //animation variables
    float timeuntileyes = 1;
    float timeuntillunge = 1;
    float timeuntilfade = 2;

	// Use this for initialization
	void Start()
    {
        state = PlayerAnimationState.Play;
        Monster = GameObject.FindGameObjectWithTag("Monster");
        Player = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update()
    {
        switch(state)
        {
            case PlayerAnimationState.DeathStart:
                timeuntileyes -= Time.deltaTime;
                if (timeuntileyes < 0)
                    state = PlayerAnimationState.DeathEyes;
                break;

            case PlayerAnimationState.DeathEyes:
                Monster.transform.position = Player.transform.position + new Vector3(Player.transform.forward.x, 0, Player.transform.forward.z).normalized;
                Monster.transform.LookAt(Player.transform.position);
                timeuntillunge -= Time.deltaTime;
                if (timeuntillunge < 0)
                    state = PlayerAnimationState.DeathLunge;
                break;
            case PlayerAnimationState.DeathLunge:
                Monster.transform.position += (Player.transform.position - Monster.transform.position).normalized * Time.deltaTime * 0.5f;
                timeuntilfade -= Time.deltaTime;
                if (timeuntilfade < 0)
                    state = PlayerAnimationState.DeathEnd;
                break;
            case PlayerAnimationState.DeathEnd:
                //End of death animation, fade into respawn
                SteamVR_Fade.Start(Color.black, 0);
                SteamVR_Fade.Start(Color.clear, 1);
                Monster.GetComponent<MonsterController>().Respawn();
                Respawn();
                break;
            case PlayerAnimationState.Play:
                break;
        }         
	}

    public void DeathCutsceneStart()
    {
        playersmoke.SetActive(true);
        state = PlayerAnimationState.DeathStart;
        teleporter.teleportOnClick = false;
        laserpointer.active = false;
        flashlight.SetActive(false);
        Monster.GetComponent<MonsterController>().state = MonsterState.Animation;
    }

    public void Respawn()
    {
        playersmoke.SetActive(false);
        transform.position = spawnpoint;
        state = PlayerAnimationState.Play;
        teleporter.teleportOnClick = true;
        laserpointer.active = true;
        flashlight.SetActive(true);

        //Reset the animation variables
        timeuntileyes = 1;
        timeuntillunge = 2;
        timeuntilfade = 3;
    }
}

public enum PlayerAnimationState { Play, DeathStart, DeathEyes, DeathLunge, DeathEnd};