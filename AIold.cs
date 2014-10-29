using UnityEngine;
using System.Collections;

public class AIold : MonoBehaviour {

	public float startTime;
	public float progress = 1f;
	public float endTime = 3f;
	public Vector3 Sideleft = new Vector3(0f, 0f, 0f);
	public Vector3 Sideright = new Vector3(0f, 0f, 0f);
	public Vector3 startPoint;
	public Vector3 endPoint;

	void Start ()

	{
		Sideleft.y = transform.position.y;
		Sideleft.z = transform.position.z;
		Sideright.y = transform.position.y;
		Sideright.z = transform.position.z;

		startPoint = Sideleft;
		endPoint = Sideright;


	}

	// Update is called once per frame
	void Update () {

		if(progress == 1f)
		{
			progress = 1 - progress;
			startTime = Time.time;
			
			if (transform.position == Sideleft)
			{
				startPoint = Sideleft;
				endPoint = Sideright;
			}
			
			if (transform.position == Sideright) 
			{
				startPoint = Sideright;
				endPoint = Sideleft;
				
			}
		}

		progress = (Time.time - startTime) / endTime;
		transform.localPosition = Vector3.Lerp(startPoint, endPoint, progress);
		if (progress >= 1)
		{
			progress = 1;
		}


	}
}
