/**
 * This class controls the Butterfly's/Objects behavior. Once it receives a location it moves 
 * toward that location.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButterflyBehavior : MonoBehaviour {
    private Vector3 destination;
    private bool destSet;

	// Use this for initialization
	void Start () {
        destSet = false;
	}
	
	// Update is called once per frame
    /**
     * Every frame it updates where the butterfly should go by using moveTowards
     */
	void Update () {
        Debug.Log("destSet is " + destSet);
        //if (destSet)
        //{
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, destination, Time.deltaTime);
            Debug.Log("Upading position from " + this.transform.position + " to " + destination);
            //this.gameObject.transform.position = destination;
            Debug.Log( this.transform.position + " to " + destination);
        //}
	}


    /**
     * Sets the destination of the object.
     * Would normally be called my Spawner script that receives the location of the other user.
     */
    public void SetDest(Vector3 dest)
    {
        if (!destSet)
        {
            Debug.Log("current pos is " + this.gameObject.transform.position);
            Debug.Log("Client butterfly dest being set to " + dest);
            destination = dest;
            destSet = true;
        }
    }



}
