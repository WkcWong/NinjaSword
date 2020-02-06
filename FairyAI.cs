using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyAI : MonoBehaviour {

    private Transform player;
    private Animator anim;
    private UnityEngine.AI.NavMeshAgent nav;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

	void Update ()
    {
        nav.SetDestination(player.position);
    }
}
