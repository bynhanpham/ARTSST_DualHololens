using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharingStageBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.transform.position = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
