using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class FairyShooting : MonoBehaviour {

    public GameObject SpawnObj;
    public Transform boss;
    public AudioClip laserSound;
    private float shot = 0f;
    private bool shooting = false;

    void Awake ()
    {
        //boss = GameObject.Find("NSpoint").transform;
    }

	void Update ()
    {
        if (CnInputManager.GetButtonUp("Shoot") || Input.GetButtonUp("Shoot"))
        {
            shooting = false;
        }
        if (shooting && shot  >= 0.1f)
        {
            Instantiate(SpawnObj, transform.position, transform.rotation);
            shot = 0;
            AudioSource.PlayClipAtPoint(laserSound, transform.position);
        }
        shot += Time.deltaTime;
        transform.LookAt(boss);
        if (CnInputManager.GetButtonDown("Shoot") || Input.GetButtonDown("Shoot"))
        {
            shooting = true;
        }
        
    }
}
