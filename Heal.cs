using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {

    private GameObject player;
    private PlayerHealth PlayerHealth;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = player.GetComponent<PlayerHealth>();
    }
	
	void Update ()
    {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && PlayerHealth.health > 0 && PlayerHealth.health <= 100)
        {
            PlayerHealth.TakeDamage(-Time.deltaTime * 2);
        }
    }
}
