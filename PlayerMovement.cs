using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 inPos;
    private Vector3 dir;
    private Animator anim;
    private float turnSmooth = 15f;

    private float h;
    private float v;

    private float jh;
    private float jv;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = CnInputManager.GetAxisRaw("Horizontal");
        float v = CnInputManager.GetAxisRaw("Vertical");
        inPos = new Vector3(h, 0, v);
        if (h != 0 || v != 0)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
        UpdateMovement();
        footsound();
    }

 

    void UpdateMovement()
    {
        if (inPos != Vector3.zero && dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSmooth * Time.deltaTime);
        }
        Transform camTrans = Camera.main.transform;
        Vector3 camForward = camTrans.TransformDirection(Vector3.forward);
        camForward.y = 0;
        camForward = camForward.normalized;
        Vector3 camRight = new Vector3(camForward.z, 0, -camForward.x);
        float cv = CnInputManager.GetAxisRaw("Vertical");
        float ch = CnInputManager.GetAxisRaw("Horizontal");
        dir = ch * camRight + cv * camForward;
    }

    void footsound()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            { GetComponent<AudioSource>().Play(); }
        }
        else
        { GetComponent<AudioSource>().Stop(); }
    }
}