using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Carema : MonoBehaviour {

    public Transform player;
    public Transform boss;
    private bool locking = false;
    public GameObject lockon;
    //for cam1
    private float posX;
    private float posZ;
    //for cam2
    private float distance = 7.5f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float Y_ANGLE_MIN = -70f;
    private float Y_ANGLE_MAX = 0f;

    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //boss = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    void Update()
    {
        if (CnInputManager.GetButtonDown("SwitchCam"))
        {
            /* if (state == true) {state = false;} else {state = true;} */
            locking = (locking) ? false : true;
        }
        currentX += CnInputManager.GetAxis("Mouse X");
        currentY += CnInputManager.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        /*if(!state && !EventSystem.current.IsPointerOverGameObject(CnInputManager.GetTouch(0).fingerId))
        {
            currentX += CnInputManager.GetAxis("Mouse X");
            currentY += CnInputManager.GetAxis("Mouse Y");
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }*/

            if (locking)
            {
                lockon.SetActive(true);
                Vector3 pos = player.position - boss.position;
                posX = Mathf.Clamp(pos.x, -4f, 4f);
                posZ = Mathf.Clamp(pos.z, -4f, 4f);
                Vector3 changePos = new Vector3(posX, 0f, posZ);
                transform.position = player.position + changePos;
                transform.LookAt(boss);
            }
            if (!locking)
            {
                lockon.SetActive(false);
                Vector3 dir = new Vector3(0, 0, -distance);
                Vector3 playerpos = player.position;
                Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);
                playerpos.y += 3f;
                transform.position = playerpos;
                transform.position += rotation * dir;
                transform.LookAt(player.position);
            }

    }
}


