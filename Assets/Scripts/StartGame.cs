using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject Player;
    public GameObject bgm;
    public static StartGame s;
    public Timer countDown = new Timer();

	// Use this for initialization
	void Start () {
        s = this;
        if (GameObject.FindGameObjectWithTag("BGM") == null)
        {
            GameObject BGMusic = Instantiate(bgm.gameObject, Player.transform.position, transform.rotation) as GameObject;
        }
        GameObject.Find("Player").GetComponent<Control>().isControllable = false;
        guiText.text= "CLIMB";
		Souls EquippedSoul = Player.GetComponent<Souls> ();
		EquippedSoul.BroadcastMessage("BaseSoul");
        EquippedSoul.Energy = 1;
        Time.timeScale = 1;
        //Player.GetComponent<Character>().startPos = Player.transform.position;
        countDown.setTimer(1);
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
