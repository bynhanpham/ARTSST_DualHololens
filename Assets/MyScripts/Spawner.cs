/**
 * This is the object that is spawned server side. It is then used 
 * instantiate things such as the butterfly obj on the client side 
 * for all users.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private Vector3 otherUserPos;   //Position of other user.
    private Vector3 startingPos;    //Starting position of this obj
    private Vector3 myCameraPos;    //The user's position
    private int UserID;             //ID to differentiate who instantiated the object. 
    private bool posSet;            //True if otherUserPos gets set to a value
    private Vector3 dest;           //destination of where the soon to be instantiated object should go
    private bool destSet;           //True if the destination has been set to a value
    public GameObject butterfly;    //GameObject that this obj instantiates
    private Quaternion rot;

    // Use this for initialization
    void Start () {
        startingPos = this.gameObject.transform.position;
        myCameraPos = Camera.main.transform.position;
        otherUserPos = new Vector3(0, -100000, 0);
        UserID = 0;
        posSet = false;
        destSet = false;
	}
	
	// Update is called once per frame
	void Update () {
        ButterflyDest();
        //Can only run once ButterflyDest() actually recieves a location to send the instantiated obj
        if (destSet)
        {
            //Quaternion rot;
            //Deciding rotation that instantiated obj should spawn with based on UserID and your current camera rotation
            /*
            if(UserID == 1)
            {
                rot = Camera.main.transform.rotation;
            }
            else
            {
                rot = Quaternion.Euler(-Camera.main.transform.rotation.eulerAngles);
            }
            */
            Debug.Log("Instantiating client butterfly");

            //Instantiates obj at position of person who gave command.
            GameObject butterfree = Instantiate(butterfly, startingPos, rot);
            //Gives the obj the destintation location
            butterfree.GetComponent<ButterflyBehavior>().SetDest(dest);
            //Destroys the spawner obj so it stops receiving updates and causes less lag.
            SelfDestruct(this.gameObject);
        }
	}

    /**
     * Sets the otherUsersPos. It is called by the RemoteHeadManager script since
     * that is the script that recieves the position of the other user.
     */
    public void SetOtherUserPos(Vector3 pos)
    {
        Debug.Log("Inside SetOtherUserPos()");
        //At start otherUserPos.y is hard set to -100000 because that would never realistically happen
        if(otherUserPos.y == -100000)
        {
            otherUserPos = pos;
            //Allows for ButterflyDest to start running when Update() calls it.
            posSet = true;
        }
    }

    /**
     * Decides the location the instantiated obj should go to by
     * comparing the distance between this obj and each user. It goes
     * to the location that is furthest away
     */
    private void ButterflyDest()
    {
        Debug.Log("Inside ButterflyDest()");
        if (posSet)
        {
            if (UserID == 0)
            {
                Debug.Log("SettingDest");
                //Checking to see which distance is furtherest
                if(Vector3.Distance(startingPos, otherUserPos) > Vector3.Distance(startingPos, myCameraPos))
                {
                    //Case where you instatiated the object. 
                    UserID = 1;
                    //Obj should go to other user
                    rot = Camera.main.transform.rotation;
                    dest = (otherUserPos + new Vector3(0,0,2));
                    destSet = true;
                }
                else
                {
                    //Case where other user instantiated the object.
                    UserID = 2;
                    //The obj should go to you.
                    rot = this.gameObject.transform.rotation;
                    dest = myCameraPos;
                    destSet = true;
                }
            }
        }
    }

    /**
     * Destroys obj
     */
    private void SelfDestruct(GameObject obj)
    {
        Debug.Log("Object go boom");
        Destroy(obj);
    }
}
