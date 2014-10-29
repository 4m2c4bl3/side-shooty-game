using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {

		Souls EquippedSoul = Player.GetComponent<Souls> ();
		EquippedSoul.BroadcastMessage("BaseSoul");
		Destroy(gameObject);
	}

}
