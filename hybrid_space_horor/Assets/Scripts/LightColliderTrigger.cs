using UnityEngine;
using System.Collections;

public class LightColliderTrigger : MonoBehaviour {

    public MonsterController m;

    // Use this for initialization
    void Start()
    {
        m = GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        //If a monster collides with the light, change it's state to Flight
        MonsterController monster = other.GetComponent<MonsterController>();
        if (monster != null)
            monster.EnterLight(transform.position, other.gameObject.transform.position);

        if (other.name == "ColliderBody")
            m.playerInLight = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "ColliderBody")
            m.playerInLight = false;
    }
}
