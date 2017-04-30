using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampenVelocity : MonoBehaviour {

    Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rigidBody.velocity *= 0.9f;	
	}
}
