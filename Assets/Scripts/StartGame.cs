using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {

		Souls EquippedSoul = Player.GetComponent<Souls> ();
		EquippedSoul.BroadcastMessage("BaseSoul");
        EquippedSoul.Energy = 1;
        Player.GetComponent<Character>().startPos = Player.transform.position;
		Destroy(gameObject);
	}

}
