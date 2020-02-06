using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {

    public ParticleSystem stillSHOOT;
    public ParticleSystem destroySHOOT;
    public float dam = 20f;
    private GameObject player;
    private PlayerHealth PlayerHealth;
    private float time = 0f;
    private SphereCollider col;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = player.GetComponent<PlayerHealth>();
        col = GetComponent<SphereCollider>();
        StartCoroutine(move());
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 10f)
        {
            StartCoroutine(die());
        }
        else
        {
            StartCoroutine(move());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            takeahit(PlayerHealth, dam);
            StartCoroutine(die());
        }     
    }

    IEnumerator move()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.forward * Time.deltaTime * 5f);
    }

    void takeahit(PlayerHealth a, float d)
    {
        a.TakeDamage(d);
    }

    IEnumerator die()
    {
        stillSHOOT.Stop();
        destroySHOOT.Play();
        col.enabled = false;
        transform.Translate(Vector3.forward * 0f);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
