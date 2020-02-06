using System.Collections;
using UnityEngine;
using CnControls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour {

    public Camera cam;
    public GameObject boss;
    public GameObject player;
    public GameObject[] obj;
    private bool yet = false;
    public GameObject info;

    public GameObject menu;

    private void Awake()
    {
        cam.GetComponent<Carema>().enabled = false;
        boss.GetComponent<EnemyAI>().enabled = false;
        boss.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0;
        player.GetComponent<PlayerAction>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(false);
        }
        Vector3 stPos = new Vector3(1f, 6f, -7f);
        cam.transform.position = stPos;
    }

    void Update ()
    {
        if (!yet)
        {
            info.SetActive(true);
            StartCoroutine(starting());
        }
        else
        {
            info.SetActive(false);
        }

        //for pause
        if (CnInputManager.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                menu.SetActive(true);

            }
            else
            {
                Time.timeScale = 1;
                menu.SetActive(false);
            }
        }
        //for restart
        if (CnInputManager.GetButtonDown("Restart"))
        {
            Time.timeScale = 1;
            StartCoroutine(restart());
        }
        //for quit
        if (CnInputManager.GetButtonDown("Quit"))
        {
            Time.timeScale = 1;
            StartCoroutine(quit());
        }
    }
    IEnumerator restart()
    {
        float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Scene");
    }
    IEnumerator quit()
    {
        float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Opening");
    }
    IEnumerator starting()
    {
        Vector3 newPos = new Vector3(1f, 5f, -32.5f);
        yield return new WaitForSeconds(6f);  
        cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, Time.deltaTime);
        yet = true;
        yield return new WaitForSeconds(6f);
        cam.GetComponent<Carema>().enabled = true;
        boss.GetComponent<EnemyAI>().enabled = true;
        boss.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 1.5f;
        player.GetComponent<PlayerAction>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(true);
        }
    }
}
