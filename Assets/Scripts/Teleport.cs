﻿using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
    Vector3 to;
    Timer jumpPause = new Timer();
    bool porting = false;
	void Start () {
        to = GameObject.Find("TeleTo").transform.position;
	}

    void OnTriggerEnter(Collider collision)
    {
                
        if (collision.gameObject.name == "Player")
        {
            jumpPause.setTimer(1);
            GameObject.Find("Player").GetComponent<Control>().movement = Vector3.zero;
            GameObject.Find("Player").GetComponent<Control>().isControllable = false;
            porting = true;
        }
    }

    void Update ()
    {
        if (jumpPause.Ok() && porting)
        {
            GameObject.Find("Player").GetComponent<Control>().isControllable = true;
            GameObject.Find("Player").GetComponent<Control>().grounded = false;
            GameObject.Find("Player").transform.position = to;
            porting = false;
            
        }
    }
	
}
