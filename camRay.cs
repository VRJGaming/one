using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class camRay : MonoBehaviour {

	public  GameObject gaze;
	GameObject hitObj;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{

		RaycastHit RT;

		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out RT)) {
			gaze.transform.position = RT.point;		
		}
	}
}