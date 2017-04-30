﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainMove : MonoBehaviour {

    public Transform bucketBottom;
    public Transform transform;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, bucketBottom.position, speed);
	}
}
