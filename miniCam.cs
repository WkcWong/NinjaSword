using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniCam : MonoBehaviour {

    private Transform player;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
	}
}
