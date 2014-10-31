using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
    public GameObject toset;
    Vector3 to;
    Timer jumpPause = new Timer();
    bool porting = false;
    public GameObject player;
	void Start () {

        to = toset.transform.position;
        player = GameObject.Find("Player");
	}

    void OnTriggerEnter(Collider collision)
    {
                
        if (collision.gameObject.name == "Player")
        {
            jumpPause.setTimer(1);
            Control.mainControl.movement = Vector3.zero;
            Control.mainControl.isControllable = false;
            porting = true;
        }
    }

    void Update ()
    {
        if (jumpPause.Ok() && porting)
        {
            Control.mainControl.isControllable = true;
            Control.mainControl.grounded = false;
            player.transform.position = to;
            player.GetComponent<Character>().startPos = to;
            porting = false;
            
        }
    }
	
}
