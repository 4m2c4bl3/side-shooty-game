using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject Player;
    Timer countDown = new Timer();

	// Use this for initialization
	void Start () {

        GameObject.Find("Player").GetComponent<Control>().isControllable = false;
        guiText.text= "CLIMB";
		Souls EquippedSoul = Player.GetComponent<Souls> ();
		EquippedSoul.BroadcastMessage("BaseSoul");
        EquippedSoul.Energy = 1;
        Player.GetComponent<Character>().startPos = Player.transform.position;
        countDown.setTimer(2);
	}

    void Update ()
    {
        if (countDown.Ok())
        {
            Destroy(gameObject);
            GameObject.Find("Player").GetComponent<Control>().isControllable = true;
        }
    }

}
