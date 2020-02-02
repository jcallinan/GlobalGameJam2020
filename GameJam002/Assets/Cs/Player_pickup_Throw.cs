﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_pickup_Throw : MonoBehaviour
{
    public float speed = 20;
    public bool canHold = true;
    public GameObject ball;
    public Transform guide;

    void Update()
    {

        ////mousebtn to pickup, throw:
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (!canHold)
        //        throw_drop();
        //    else
        //        Pickup();
        //}

        //if (!canHold && ball)
        //{
        //    ball.transform.position = guide.position;
        //}

        ////walk-over to pickup, E to throw:
        if (Input.GetKey("e"))
        {
            if (!canHold)
            { //if holding ball, throw or drop it
                throw_drop();
            }
            //else
            //{//needs to be out of this if stmt
            //    
            //}

        }

        //if (canHold && ball) //MOVED this to collision method
        //{//handles picking up the object
        //    Pickup();
        //}

        if (!canHold && ball)
        { //this handles the object (after its being held) remaining in the 'hand'
            ball.transform.position = guide.position;
        }



    }

    //We can use trigger or Collision
    void OnTriggerEnter(Collider col)
    {

        Debug.Log("on trigger enter ");

        if (col.gameObject.tag == "wire")
            if (!ball) // if we don't have anything holding { 
            {
                ball = col.gameObject;
            }
            
        if (canHold && ball)
        {/////////////////handles picking up the object
            if (Input.GetKey("e"))
            {//dont pick it up unless E is pressed
                Pickup();
            }
            
        }

    }

    //We can use trigger or Collision
    void OnTriggerExit(Collider col)
    {
        Debug.Log("on trigger exit ");
        if (col.gameObject.tag == "ball")
        {
            if (canHold)
                ball = null;
        }
    }


    private void Pickup()
    {
        if (!ball)
        {
            return;
        }
            

        //We set the object parent to our guide empty object.
        ball.transform.SetParent(guide);

        //Set gravity to false while holding it
        ball.GetComponent<Rigidbody>().useGravity = false;

        //we apply the same rotation our main object (Camera) has.
        ball.transform.localRotation = transform.rotation;
        //We re-position the ball on our guide object 
        ball.transform.position = guide.position;

        canHold = false;
    }

    private void throw_drop()
    {
        if (!ball)
            return;

        //Set our Gravity to true again.
        ball.GetComponent<Rigidbody>().useGravity = true;
        // we don't have anything to do with our ball field anymore
        ball = null;
        //Apply velocity on throwing
        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        //Unparent our ball
        guide.GetChild(0).parent = null;
        canHold = true;
    }
}//class

