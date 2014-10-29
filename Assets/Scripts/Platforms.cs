using UnityEngine;
using System.Collections;

public class Platforms : MonoBehaviour {

    public bool upDown;
    public bool leftRight;
    public float transDistance;
    public bool damaging;
    public bool disappearing;
    public float safeLength;
    public float warningLength;
    public float dangerLength;
    public int damageGiven;
	public float damageSpeed;
    Vector3 startPos;
    Vector3 endPos;
    float progress = 1f;
    public float moveSpeed = 10f;
    int stage = 0;
    float startTime = 0;
    Timer damageTimer = new Timer();
	Timer waitTimer = new Timer();
	Timer moveProg = new Timer();
 

	// Use this for initialization
	void Start () {

        startPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        damageTimer.setTimer(safeLength);

		if (upDown)
		{
			endPos = new Vector3(startPos.x, (startPos.y + transDistance), startPos.z);			
		}
		if (leftRight)
		{
			endPos = new Vector3((startPos.x + transDistance), startPos.y, startPos.z);
		}
        
        	}
	
	// Update is called once per frame
	void Update () {

        
        if (upDown || leftRight)
        {
            lerpMove(); 
        }
        
        if (damaging)
        { 
            if (stage == 0 && damageTimer.Ok())
            {                
                damageTimer.setTimer(warningLength);
                damRun(1);
            }
            else if (stage == 1 && damageTimer.Ok())
            {
                damageTimer.setTimer(dangerLength);
				waitTimer.setTimer(damageSpeed);
                damRun(2);
            }
            else if (stage == 2 && damageTimer.Ok())
            {
                damageTimer.setTimer(safeLength);
                damRun(0);
            }
            
        }

		if (disappearing && stage == 2 && waitTimer.Ok ()) 
		{
				disRun(0);
				waitTimer.sleep();
		}


	}

	void OnTriggerStay(Collider collisionInfo)
    {

		if (disappearing && collisionInfo.gameObject.tag == "Player")
        {
            if (stage == 0 && damageTimer.Ok())
            {
                damageTimer.setTimer(warningLength);
                disRun(1);
                
            }
            else if (stage == 1 && damageTimer.Ok())
            {
                damageTimer.setTimer(dangerLength);
                disRun(2);
            }
            else if (stage == 2 && damageTimer.Ok())
            {
                damageTimer.setTimer(safeLength);
                disRun(0);
            }
        }

		if (damaging && stage == 2 && waitTimer.Ok() && collisionInfo.gameObject.tag == "Player") 
		{
			floorDamage(damageGiven);

				}

    }

	void floorDamage (int strength)
	{

		waitTimer.setTimer(damageSpeed);
		var player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<Character>().pureDamaged(strength);

		}

	void OnTriggerExit (Collider collider)
	{
		bool notDone = stage == 0 || stage == 1;
        if (disappearing && collider.gameObject.tag == "Player" && notDone)
		{
            disRun(0);				
		}
	}



    void damRun(int callstage)
    {
        stage = callstage;

        if (stage == 0)
        {
            renderer.material.color = Color.white;
        }
        else if (stage == 1)
        {
            renderer.material.color = Color.yellow;
        }
        else if (stage == 2)
        {
            renderer.material.color = Color.red;
        }
        
    }

    void disRun(int callstage)
    {
        stage = callstage;
        Material visibility = new Material(gameObject.renderer.material);
        Color vis = visibility.color;
        
        if (stage == 0)
        {
            vis.a = 1;
            collider.enabled = true;
        }
        else if (stage == 1)
        {
            vis.a = 0.5f;
        }
        else if (stage == 2)
        {
            vis.a = 0;
            collider.enabled = false;
			waitTimer.setTimer(5);

        }
        visibility.color = vis;
        gameObject.renderer.material = visibility;
    }

    void lerpMove ()
    {
		progress = moveProg.progress (startTime, moveSpeed);
        transform.position = Vector3.Lerp(startPos, endPos, progress);
		if (progress >= 1)
        {
			progress = 1;
        }

		if (upDown && progress == 1)
		{
			progress = 1 - progress;
			startTime = Time.time;
			float tempy = startPos.y;
			startPos.y = gameObject.transform.position.y;
			endPos.y = tempy;

		}
		else if (leftRight && progress == 1)
		{
			progress = 1 - progress;
			startTime = Time.time;
			float tempx = startPos.x;
			startPos.x = gameObject.transform.position.x;
			endPos.x = tempx;
		}
    }
}
