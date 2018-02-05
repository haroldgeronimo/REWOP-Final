using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotate : MonoBehaviour {

	public Transform playerTransform;
	public float offset_y = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 eulerAngeles = transform.eulerAngles;
		eulerAngeles.y = 0;
		eulerAngeles.z = 0;
		transform.eulerAngles = eulerAngeles;
	}
}
