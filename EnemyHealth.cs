using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public float health = 1000f;
    private Animator anim;
    private bool die = false;
    private EnemyAI EnemyAI;
    public Slider EnemyHP;

    void Awake()
    {
        anim = GetComponent<Animator>();
        EnemyAI = GetComponent<EnemyAI>();
        EnemyHP.maxValue = health;
        EnemyHP.value = health;
    }

    void Update()
    {
        if (health <= 0f)
        {
            if (!die)
                Dying();
            else
            {
                Dead();
            }
        }
    }
    void Dying()
    {
        anim.SetTrigger("dying");
        EnemyAI.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0f;
        die = true;
    }
    void Dead()
    {
        
    }

    public void TakeDamage(float d)
    {
        health -= d;
        EnemyHP.value = health;
    }
}