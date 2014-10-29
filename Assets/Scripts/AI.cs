using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public Vector3 View = new Vector3 (1f, 0f, 0f);
	public float Speed = 10.0f;
    Vector3 startPos;
    Vector3 endPos;
    float startTime;
    float progress;
    public float flySpeed;
    public float flyDist;
    Timer moveProg = new Timer();
    void Start ()
    {
        if (gameObject.name == "FlyingEnemy")
        {
            startPos = gameObject.transform.position;
            endPos = gameObject.transform.position;
            endPos.x += flyDist;
            startTime = Time.time;
        }
    }

	void Update ()
	{
        if (gameObject.name == "Enemy")
        { 

    		transform.Translate(View * Speed * Time.deltaTime);
    
        }
        if (gameObject.name == "FlyingEnemy")
        {
            lerpFly();
        }
	}


	void OnTriggerEnter (Collider other)
	{

		if (other.tag == "EnemyBoundary" && gameObject.name == "Enemy") 
		{
			View = -View;

				}
	}

    void lerpFly ()
    {
        if (progress >= 1)
        {
            progress = 1;
        }
        if (progress == 1)
        {
            progress = 1 - progress;
            startTime = Time.time;
            float tempx = startPos.x;
            startPos.x = endPos.x;
            endPos.x = tempx;

        }
        progress = moveProg.progress(startTime, flySpeed);
        transform.position = Vector3.Lerp(startPos, endPos, progress);
          }


}
