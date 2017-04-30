using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireworks : MonoBehaviour {

    public List<GameObject> prefabs;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("c"))
            SpawnPrefab();
	}
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("asdfasdf");
        if (collider.tag == "ball") 
           SpawnPrefab();
    }
    private void SpawnPrefab()
    {
        //This will spawn them at (0,0,0) with (0,0,0) rotation
        for (int c = 0; c < prefabs.Count; c++)
        {
            GameObject GO = Instantiate(prefabs[c], GetComponent<Transform>().position, Quaternion.identity, GetComponent<Transform>());
            
        }
        //Failsafe, if we can't get the ball in the cupx

    }
}
