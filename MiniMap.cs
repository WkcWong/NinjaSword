using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public Transform obj;

    private void Awake()
    {
    }

    void Update ()
    {
        transform.position = new Vector3(obj.position.x, 2f, obj.position.z);
	}
}
