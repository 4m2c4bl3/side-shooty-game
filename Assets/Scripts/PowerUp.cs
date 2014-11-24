using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    GameObject Player;
	public bool Heal;
	public int healAmount;
	public bool increaseSoul;
	public string increaseSoulname;
 	public bool hangTime;
	public bool Power2;
	public bool Power3;
    float destTime = 2;
    Timer destTimer = new Timer();

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

	void Healrun ()
	{
		//healin
		Souls PlayerHP = Player.GetComponent<Souls> ();
		PlayerHP.CurHP += healAmount;
		if (PlayerHP.CurHP >= PlayerHP.MaxHP) 
		{
			PlayerHP.CurHP = PlayerHP.MaxHP;
				}   

        destTimer.setTimer(destTime);
        pausedDestroy();
		}
    void Update()
    {
        if (gameObject.collider.isTrigger == false)
        {
            pausedDestroy();
        }
    }
    void pausedDestroy()
    {
     gameObject.collider.isTrigger = false;
        if (destTimer.Ok())
        {
            Destroy(gameObject);
        }
    }



	void increaseSoulrun ()
	{
		//raise soul to specified level
		Souls EquippedSoul = Player.GetComponent<Souls> ();
        EquippedSoul.BroadcastMessage(increaseSoulname);
        destTimer.setTimer(destTime);
        pausedDestroy();
		}
	void hangTimerun ()
	{
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInParent<Control>().hangYes = true;
		//grants power 1
        Scores.mainScore.powerUps++;
        GetComponentInChildren<TextMesh>().text = "Hold K to jump longer.";
        destTimer.setTimer(destTime);
        pausedDestroy();
		
	}
	void Power2run ()
	{
		//grants power 2
        Scores.mainScore.powerUps++;
        destTimer.setTimer(destTime);
        pausedDestroy();
	}
	void Power3run ()
	{
		//grants power 3
        Scores.mainScore.powerUps++;
        destTimer.setTimer(destTime);
        pausedDestroy();
	}

    void OnTriggerEnter(Collider other)
	{
       	if (other.gameObject.name == "Player") 
		{
            playSound.p.Play(3);
			if (Heal)
			{
				Healrun();
			}
			if (increaseSoul)
			{
				increaseSoulrun();
			}
			if (hangTime)
			{
				hangTimerun();
			}
			if (Power2)
			{
				Power2run();
			}
			if (Power3)
			{
				Power3run();
			}
        }
		}


}
