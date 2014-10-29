using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public Vector3 View = new Vector3 (1f, 0f, 0f);
	public float Speed = 10.0f;

	void Update ()
	{
		transform.Translate(View * Speed * Time.deltaTime);
	}


	void OnTriggerEnter (Collider other)
	{

		if (other.tag == "EnemyBoundary") 
		{
			View = -View;

				}
	}


}
