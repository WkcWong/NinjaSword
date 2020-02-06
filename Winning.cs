using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winning : MonoBehaviour {

    public GameObject finish;
    public GameObject timeUsed;
    public GameObject score;
    public GameObject rest;
    public GameObject qui;
    public Carema cam;
    public int min;
    public int s;
    Text text;

    void Awake ()
    {
        text = score.GetComponent<Text>();
        text.text = min + "min " + s + "s";
    }
    private void Update()
    {
        StartCoroutine(show());
        if (!GetComponent<AudioSource>().isPlaying)
        { GetComponent<AudioSource>().Play(); }
        cam.GetComponent<AudioSource>().Stop();
    }

    IEnumerator show()
    {
        yield return new WaitForSeconds(5f);
        finish.SetActive(false);
        timeUsed.SetActive(true);
        yield return new WaitForSeconds(2f);
        score.SetActive(true);
        yield return new WaitForSeconds(1f);
        rest.SetActive(true);
        qui.SetActive(true);
    }
}
