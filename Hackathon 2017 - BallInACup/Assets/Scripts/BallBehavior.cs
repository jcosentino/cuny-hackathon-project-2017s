using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {
    
    public Transform transform;
    public Rigidbody rigidBody;
    public Transform cup; //transform of the cup
    public float outOfBoundsMaxAcceleration; //"tension" of the rope
    public float ropeLength; //used for checking if the ball is out of bounds
    public float maxDistance; //distance at which acceleration is maximum (physics don't normally work this way)
    
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if( Mathf.Abs( Vector3.Distance(transform.position, cup.position)) > ropeLength) //If distance between ball and cup is too great
        {
            //Accelerate ball towards cup
            rigidBody.velocity += Vector3.Normalize(cup.position - transform.position) 
                * outOfBoundsMaxAcceleration * 
                MagnitudeOfAcceleration(); //Normal from ball to cup, times speed, times magnitude

        }
	}

    float MagnitudeOfAcceleration()
    {
        //This magnitude maps the acceleration from 0 to outOfBoundsMaxAcceleration when its distance from the bucket is
        //greater than ropeLength
        float magnitude = 1 - ( (maxDistance - ropeLength) - Mathf.Clamp( //Clamp distance so acceleration doesn't go over 1 when it's past max distance
                                        Vector3.Distance(transform.position, cup.position) - ropeLength, 
                                        0, maxDistance -ropeLength) ) 
                                        / (maxDistance - ropeLength); 
        
        //Debug.Log(magnitude.ToString());
        return magnitude;
    }
    

}
