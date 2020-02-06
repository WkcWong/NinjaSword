using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

    public float dam = 10;
    public GameObject me;
    private Transform player;
    private Animator anim;
    private PlayerHealth PlayerHealth;
    private UnityEngine.AI.NavMeshAgent nav;
    private float attacked = 3f;
    public float randomAction;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        PlayerHealth = player.gameObject.GetComponent<PlayerHealth>();
        randomAction = Random.Range(0f, 2f);
        anim.SetFloat("action", randomAction);
    }
	
	void Update ()
    {
        attacked += Time.deltaTime;
        float distance = Vector3.Distance(player.position, transform.position);
        Vector3 relativePos = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
        nav.speed = (randomAction > 1f) ? 2.5f : 0.5f;

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (distance > 1.2)
            {
                anim.SetBool("move", true);
                nav.SetDestination(player.position);
            }
            else
            {
                anim.SetBool("move", false);
                anim.SetTrigger("attack");
            }
        }
        else
        {
            nav.speed = 0f;
            anim.SetBool("move", false);
        }
        //killed
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
        {
            GetComponent<ZombieAI>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            nav.speed = 0f;
            StartCoroutine(Die());
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && attacked > 3f
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            takeahit(PlayerHealth, dam);
            attacked = 0;
        }
    }

    void takeahit(PlayerHealth a, float d)
    {
        a.TakeDamage(d);
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        Destroy(me);
    }
}
