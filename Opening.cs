using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{

    public GameObject text;
    public AudioClip clickSound;
    private Animator animt;

    private void Awake()
    {
        animt = text.GetComponent<Animator>();
    }

    void Update()
    {
        if (CnInputManager.GetButtonDown("Restart"))
        {
            StartCoroutine(go());
            animt.SetTrigger("clicked");
            AudioSource.PlayClipAtPoint(clickSound, transform.position);
        }
    }
    IEnumerator go()
    {
        yield return new WaitForSeconds(1f);
        float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Scene");
    }
}


