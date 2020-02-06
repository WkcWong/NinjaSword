using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinControl : MonoBehaviour {

    public Camera cam;
    public GameObject cav;
    public GameObject win;
    private GameObject[] zombies;
    private GameObject boss;
    private GameObject spawnPoint1;
    private GameObject spawnPoint2;
    private GameObject healPoint;
    private GameObject player;

    private GameObject[] weapons;
    private PlayerHealth PlayerHealth;
    private EnemyHealth EnemyHealth;
    private float time = 0f;

    void Awake ()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        spawnPoint1 = GameObject.Find("SpawnZombie (1)");
        spawnPoint2 = GameObject.Find("SpawnZombie (2)");
        healPoint = GameObject.Find("healPoint");
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = player.GetComponent<PlayerHealth>();
        EnemyHealth = boss.GetComponent<EnemyHealth>();
    }

	void Update ()
    {
        zombies = GameObject.FindGameObjectsWithTag("Enemy");
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        time += Time.deltaTime;
        if (EnemyHealth.health <= 0)
        {
            //for enemy
            for (int i = 0; i < zombies.Length; i++)
            {
                Destroy(zombies[i].gameObject);
            }
            Destroy(spawnPoint1);
            Destroy(spawnPoint2);
            Destroy(healPoint);
            //for player
            PlayerHealth.health = 100f;
            PlayerHealth.enabled = false;
            player.GetComponent<PlayerAction>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Animator>().SetBool("win", true);
            player.transform.LookAt(cam.transform);
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].gameObject.SetActive(false);
            }
            //for canvas
            cav.SetActive(false);
            win.SetActive(true);
        }
        win.GetComponent<Winning>().min = (int)time / 60;
        win.GetComponent<Winning>().s = (int)time % 60;
    }
}
