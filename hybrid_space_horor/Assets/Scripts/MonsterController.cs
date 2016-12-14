using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

    public GameObject player;
    float speed = 5f;
    float flightradius, flighttimer;
    public MonsterState state;
    [HideInInspector] public Vector3 lastLight;
    Vector3 flightdir;
    [HideInInspector] public bool playerInLight;
    NavMeshAgent pathing;
    public Vector3 spawnpoint;
    public PlayerController playercontroller;

	// Use this for initialization
	void Start () {
        state = MonsterState.Idle;
        pathing = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 playerdirection = (player.transform.position - transform.position).normalized;
        
        //Finite State Machine
        Ray ray = new Ray(player.transform.position, (transform.position - player.transform.position));
        RaycastHit hitInfo = new RaycastHit();
        switch (state)
        {
            case (MonsterState.Idle):
                //Trace a ray to see if the monster can see the player, if yes, go into pursuit    
                if (Physics.Raycast(ray, out hitInfo))
                    if(hitInfo.collider.tag == "Monster")
                        state = MonsterState.Pursuit;
                //The hmd does not have a collider, so it can not be raytraced
                //Therefore we trace the ray in reverse and see if it hits the monster
                break;

            case (MonsterState.Pursuit):
                //Set the destination of the pathing system to the player
                pathing.SetDestination(player.transform.position);

                //Turn towards the player
                transform.LookAt(player.transform);

                //If the player gets too close, they die. Start the death cutscene / respawn routine
                if ((transform.position - player.transform.position).magnitude < 0.3f)
                    playercontroller.DeathCutsceneStart();

                //Trace a ray to see if the monster can see the player, if not, go idle
                if (Physics.Raycast(ray, out hitInfo))
                    if (hitInfo.collider.tag != "Monster")
                        state = MonsterState.Idle;

                break;
            case (MonsterState.Flight):
                flighttimer -= Time.deltaTime;

                //Trace a ray to see if the monster can see the player, if not, go idle
                Physics.Raycast(ray, out hitInfo);

                if (pathing.destination == transform.position || flighttimer < 0)
                {
                    if (playerInLight && hitInfo.collider.tag == "Monster")
                        state = MonsterState.Wary;
                    else
                        state = MonsterState.Idle;
                }
                break;
            case (MonsterState.Wary):
                //Only go back into an offensive state if the player is no longer in a light
                if(!playerInLight)
                {
                    if (Physics.Raycast(ray, out hitInfo))
                        if (hitInfo.collider.tag == "Monster")
                            state = MonsterState.Idle;
                }
                break;

            case (MonsterState.Animation):
                //This is state does nothing
                //In the player death animation the PlayerController.cs scripts controls the monster
                //During the animation the monster should not move on it's own
                break;
        }
	}

    public void EnterLight(Vector3 light, Vector3 position)
    {
        state = MonsterState.Flight;
        flightradius = 12;
        flightdir = new Vector3(position.x - light.x, 0, position.z - light.z).normalized;

        flighttimer = 3;

        //Choose a point away from the light to run from
        Vector3 flighttarget = transform.position + flightradius * flightdir;
        //Fin the nearest point to the flighttarget
        NavMeshHit hit;
        NavMesh.SamplePosition(flighttarget, out hit, flightradius, 1);
        Vector3 finalPosition = hit.position;
        pathing.SetDestination(finalPosition);
    }

    public void Respawn()
    {
        transform.position = spawnpoint;
        state = MonsterState.Idle;
    }
}

public enum MonsterState { Idle, Pursuit, Flight, Wary, Animation};