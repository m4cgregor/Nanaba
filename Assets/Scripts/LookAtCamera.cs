using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    public GameObject mainCamera;

	// Use this for initialization
	void Start () {

		mainCamera = Camera.main.gameObject;

    }
	
	// Update is called once per frame
	void Update () {

       this.transform.LookAt(mainCamera.transform);
		
	}
}
