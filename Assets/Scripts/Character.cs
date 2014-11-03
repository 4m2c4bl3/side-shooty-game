using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    Souls equippedSoul;
    public float lives = 3;
    public Vector3 startPos;
    public static bool _respawn;
    Timer animTimer = new Timer();
    Timer transTimer = new Timer();

    bool respawn
    {
        get
        {
            return _respawn;
        }
        set
        {
            if (value)
            {
                if (_respawn != value)
                {
                    transTimer.setTimer(2);
                    Control.mainControl.isControllable = false;
                }
                if (transTimer.Ok())
                {
                    lives -= 1;
                    Scores.mainScore.livesLost++;
                    equippedSoul.CurHP = equippedSoul.MaxHP;
                    gameObject.GetComponent<Control>().movement = Vector3.zero;
                    transform.position = startPos;
                    Control.mainControl.isControllable = true;
                    transTimer.sleep();
                    respawn = false;
                }
            }
            _respawn = value;
        }
    }
    void Start()
    {
        equippedSoul = gameObject.GetComponent<Souls>();
        equippedSoul.CurHP = equippedSoul.MaxHP;
        startPos = transform.position;
    }


    void Damaged(int Power)
    {
        if (equippedSoul.Defence < Power)
        {equippedSoul.CurHP -= Power - equippedSoul.Defence; }
    }
	public void pureDamaged(int Power)
	{
        equippedSoul.CurHP -= Power;

	}

    void Update()
    {
        if (Scores.mainScore.completedMap == true && transTimer.Ok())
        {
            Application.LoadLevel("over");
        }
        
        if (animTimer.Ok())
        {
                Renderer renderKid = GetComponentInChildren<Renderer>();
                renderKid.material.color = Color.white;
 
        }
        if (equippedSoul.CurHP <= 0)
        { 
           // Destroy(gameObject); 
 
                if (lives >= 1)
                {
                    respawn = true;
                }
                if (lives <= 0)
                {
                    transTimer.setTimer(1);
                    if (transTimer.Ok())
                    {
                        Application.LoadLevel("over");
                        transTimer.sleep();
                    }
                }
        }
        
        
    }

    
    void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.name == "LevelComplete")
        {
            Scores.mainScore.completedMap = true;
            Control.mainControl.isControllable = false;
            transTimer.setTimer(1);
        }
    }

 
    void GotHit (Collision collision)
    {
        Attack enemyattack = collision.gameObject.GetComponent<Attack>();
        
        if (enemyattack != null)
        {
            Damaged(enemyattack.Strength);
        }
        if (collision.gameObject.tag == "Enemy" && gameObject.name == "Player")
        {
            Damaged(collision.gameObject.GetComponent<Souls>().Strength);
            animTimer.setTimer(0.1f);
            Renderer renderKid = GetComponentInChildren<Renderer>();
            renderKid.material.color = Color.yellow;
        }

    }

}
