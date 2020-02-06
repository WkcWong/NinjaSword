using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private EnemyHealth EnemyHealth;
    private GameObject enemy;
    private float time = 0f;
    public float dam = 1f;


    void Awake ()
    {
        enemy = GameObject.FindGameObjectWithTag("Boss");
        EnemyHealth = enemy.GetComponent<EnemyHealth>();
    }
	
	void Update ()
    {
        time += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * 20f);
        if (time >= 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            takeahit(EnemyHealth, dam);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    void takeahit(EnemyHealth a, float d)
    {
        a.TakeDamage(d);
    }
}
