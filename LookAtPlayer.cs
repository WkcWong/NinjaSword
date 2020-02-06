using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    private GameObject player;

    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	

	void Update () {
        Vector3 relPlayerPos = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relPlayerPos);
        transform.rotation = rotation;
    }
}
