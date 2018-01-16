using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardEffect : MonoBehaviour {
    public Camera main_camera;

    // Update is called once per frame
    private void Start()
    {
        main_camera = Camera.main;
    }
    void Update () {
        transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.forward, main_camera.transform.rotation * Vector3.up);
	}
}
