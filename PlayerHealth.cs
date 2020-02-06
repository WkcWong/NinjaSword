using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Slider PlayerHP;
    public Slider dePlayerHP;
    public AudioClip deadSound;
    private Animator anim;
    private bool die = false;
    private PlayerMovement PlayerMovement;
    private PlayerAction PlayerAction;
    private float timer;
    private float deHP = 0f;
    private bool skilling = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAction = GetComponent<PlayerAction>();
        PlayerHP.maxValue = health;
        PlayerHP.value = health;
    }

    void Update()
    {
        if (deHP > 0)
        {
            deHP -= Time.deltaTime * 10f;
            dePlayerHP.value -= Time.deltaTime * 10f;
        }
        else if (deHP < -10)
        {
            deHP += Time.deltaTime * 10f;
            dePlayerHP.value += Time.deltaTime * 10f;
        }

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
         PlayerMovement.enabled = false;
         PlayerAction.enabled = false;
        AudioSource.PlayClipAtPoint(deadSound, transform.position);
        die = true;
     }
     void Dead()
     {
        StartCoroutine(restart());
    }

    public void TakeDamage(float d)
    {
        if (!skilling)
        {
            health -= d;
            PlayerHP.value = health;
            deHP += d;
        }
    }
    IEnumerator restart()
    {
        yield return new WaitForSeconds(3f);
        float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 3f);
        SceneManager.LoadScene("Scene");
    }
    public void usingskill()
    {
        skilling = true;
    }
    public void endskill()
    {
        skilling = false;
    }
}