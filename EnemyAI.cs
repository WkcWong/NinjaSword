using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Animator anim;
    private GameObject player;

    public GameObject SpawnObj;
    public Transform spawnPoint;
    public AudioClip attackSound;
    private float timer = 0;
    private bool shot = false;

    private float patrolTimer;
    public Transform[] patrolWayPoints;
    private UnityEngine.AI.NavMeshAgent nav;
    private int wayPointNo;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        timer += Time.deltaTime;
        nav.destination = patrolWayPoints[wayPointNo].position;
        //for sight
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            Vector3 relPlayerPos = player.transform.position - transform.position;
            relPlayerPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relPlayerPos);
            transform.rotation = rotation;
        }
        //for move
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            anim.SetBool("move", false);
            if (timer >= 5 && shot == false
                && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.SetTrigger("magic");
                StartCoroutine(attack1());
                shot = true;
            }
        }
        else
        {
            anim.SetBool("move", true);
        }
    }
    //Attack 1
    IEnumerator attack1()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(SpawnObj, spawnPoint.position, spawnPoint.rotation);
        AudioSource.PlayClipAtPoint(attackSound, spawnPoint.transform.position);
        yield return new WaitForSeconds(5f);
        if (wayPointNo != patrolWayPoints.Length - 1)
        {
            wayPointNo++;
        }
        else
        {
            wayPointNo = 0;
        }
        timer = 0;
        shot = false;
    }
}
