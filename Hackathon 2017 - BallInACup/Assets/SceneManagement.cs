using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagement : MonoBehaviour {

    bool lastState;
    bool currentState;
	// Use this for initialization
	void Start () {
        lastState = false; currentState = false;
	}
	
	// Update is called once per frame
	void Update () {
        currentState = Input.GetKey("f"); //true if held down
        if (currentState && !lastState)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKey("1"))
            SceneManager.LoadScene(1);

        if (Input.GetKey("2"))
            SceneManager.LoadScene(2);

        //When someone presses the screen, change the scenes 
        //back and forth (or to scene 1 with the chain if it's the homescreen)
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    SceneManager.LoadScene(2);
                }
                else
                    SceneManager.LoadScene(1);
            }
        }

                lastState = currentState;
    }
}
