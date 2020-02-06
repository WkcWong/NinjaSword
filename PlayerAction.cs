using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameObject dashIcon;
    public GameObject skillIcon;
    public GameObject upIcon;
    public ParticleSystem occur;
    //for attack
    public float ad1 = 10;
    public float ad2 = 20;
    public float ad3 = 30;
    public ParticleSystem blood;
    public AudioClip slashSound;
    public AudioClip cutSound;
    private Animator anim;
    private GameObject enemy;
    private GameObject zombie;
    private EnemyHealth EnemyHealth;
    private PlayerHealth PlayerHealth;
    private bool a1, a2, a3 = false;
    private int combo = 0;
    private float lastClick = 0f;
    private float maxTime = 0.7f;
    private bool attacked = false;
    private bool two = false;
    //for dash
    public GameObject body;
    public GameObject swordL;
    public GameObject swordR;
    public ParticleSystem effect;
    public AudioClip dashSound;
    public Slider St;
    private float dasht = 0f;
    private bool dash = false;
    private float strength = 100f;
    //for jump
    public LayerMask ground;
    public AudioClip jumpSound;
    private bool jump = false;
    private CapsuleCollider col;
    private Rigidbody rb;
    private float jumpForce = 900f;
    private float g = 150f;
    private float action = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("Boss");
        EnemyHealth = enemy.GetComponent<EnemyHealth>();
        PlayerHealth = GetComponent<PlayerHealth>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (strength < 100)
        {
            strength += Time.deltaTime * 5;
            St.value = strength;
        }
        if (strength < 25f)
        {
            dashIcon.SetActive(false);
        }
        else
        {
            dashIcon.SetActive(true);
        }
        if (strength < 90f)
        {
            skillIcon.SetActive(false);
            upIcon.SetActive(false);
        }
        else if (!two && strength >= 90f)
        {
            skillIcon.SetActive(true);
        }
        else if (two && strength >= 90f)
        {
            upIcon.SetActive(true);
        }
        anim.SetBool("a1", a1);
        anim.SetBool("a2", a2);
        anim.SetBool("a3", a3);
        anim.SetBool("jump", jump);
        anim.SetBool("dash", dash);
        action += Time.deltaTime;
        //for attack
       // if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.5 && !anim.IsInTransition(0))
       // { attacked = false; }
        if (Time.time - lastClick > maxTime)
        { combo = 0; }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        { a1 = false; a3 = false;}
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        { a1 = false; a2 = false;}
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        { a2 = false; a3 = false; }
        if (CnInputManager.GetButtonDown("Attack"))
        {
            combo++;
            action = 0;
            if (combo == 1)
            {
                lastClick = Time.time;
                a1 = true;
            }
            else if (combo == 2)
            {
                lastClick = Time.time;
                a2 = true;
            }
            else if (combo == 3)
            {
                lastClick = Time.time;
                a3 = true;
            }
            else
            { combo = 0;  }
        }
        //for skill
        if (CnInputManager.GetButtonDown("Skill") && strength >= 90f)
        {
            strength -= 90f;
            if (!two)
            {
                StartCoroutine(twoSword());
            }
            else
            {
                StartCoroutine(powerUp());
            }
            
            
        }
        //for dash
        if (!dash)
        {
            if (CnInputManager.GetButtonDown("Dash") && strength >= 25f)
            {
                AudioSource.PlayClipAtPoint(dashSound, transform.position);
                body.SetActive(false);
                swordR.SetActive(false);
                effect.Play();
                dash = true;
                strength -= 25f;
                PlayerHealth.usingskill();
            }
        }
        else
        {
            dasht += Time.deltaTime;
            if (dasht > 0.35f)
            {
                body.SetActive(true);
                //swordL.SetActive(true);
                swordR.SetActive(true);
                effect.Stop();
                dasht = 0f;
                dash = false;
                PlayerHealth.endskill();
            }
        }
        //for jump && user friendly delay
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")
    || anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            if (CnInputManager.GetButtonDown("Jump") && onGround() && action > 0.2)
            {
                StartCoroutine(jumping());
                action = 0;
            }
        }
    }

    IEnumerator jumping()
    {
        AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, 0, 0);
        jump = true;
        yield return new WaitForSeconds(0.5f);
        Physics.gravity = new Vector3(0, -g, 0);
        jump = false;
    }

    private bool onGround()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(
        col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, ground);
    }

    void OnTriggerStay(Collider other)
    {
        //for boss
        if (other.gameObject.tag == "Boss" && !attacked)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                takeahit(EnemyHealth, ad1);
                StartCoroutine(attackwait(0.4f));
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                takeahit(EnemyHealth, ad2);
                StartCoroutine(attackwait(1f));
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                takeahit(EnemyHealth, ad3);
                StartCoroutine(attackwait(1f));
            }
        }

        //for zombie
        if (other.gameObject.tag == "Enemy")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") 
                || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
                || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                zombie = other.gameObject;
                zombie.GetComponent<Animator>().SetTrigger("dying");
                AudioSource.PlayClipAtPoint(cutSound, other.transform.position);
            }
        }        
    }

    void takeahit(EnemyHealth a, float d)
    {
        a.TakeDamage(d);
        AudioSource.PlayClipAtPoint(slashSound, transform.position);
        StartCoroutine(bloodEffect());
    }
    IEnumerator bloodEffect()
    {
        blood.Play();
        yield return new WaitForSeconds(0.1f);
        blood.Stop();
    }
    IEnumerator attackwait(float t)
    {
        attacked = true;
        yield return new WaitForSeconds(t);
        attacked = false;
    }
    //if no 2 swords, 2nd sword occured. Otherwise improve the power.
    IEnumerator twoSword()
    {
        PlayerHealth.usingskill();
        occur.Play();
        yield return new WaitForSeconds(1f);
        occur.Stop();
        swordL.SetActive(true);
        yield return new WaitForSeconds(1f);
        PlayerHealth.endskill();
        two = true;
    }
    IEnumerator powerUp()
    {
        PlayerHealth.usingskill();
        occur.Play();
        yield return new WaitForSeconds(2f);
        occur.Stop();
        PlayerHealth.endskill();
        ad1 *= 2f;
        ad2 *= 2f;
        ad3 *= 2f;
    }
}
