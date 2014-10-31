using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float MaxHP = 5f;
    public float CurHP = 0f;
    public Souls EquippedSoul;
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
                    CurHP = MaxHP;
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
        CurHP = MaxHP;
        startPos = transform.position;
    }


    void Damaged(int Power)
    {
        if (EquippedSoul.Defence < Power)
        { CurHP -= Power - EquippedSoul.Defence; }
    }
	public void pureDamaged(int Power)
	{
		CurHP -= Power;

	}

    void Update()
    {
        if (Scores.mainScore.completedMap == true && transTimer.Ok())
        {
            Application.LoadLevel("over");
        }
        
        if (animTimer.Ok())
        {
            if (gameObject.name == "FlyingEnemy" || gameObject.name == "Player")
            {
                Renderer renderKid = GetComponentInChildren<Renderer>();
                renderKid.material.color = Color.white;
            }

            else
            {
                renderer.material.color = Color.white;
            }
        }
        if (CurHP <= 0)
        { 
           // Destroy(gameObject); 
            
            if (gameObject.name == "Player")
            {
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
            if (gameObject.tag == "Enemy")
            {
                Destroy(gameObject); 
                Scores.mainScore.enemiesKilled++;
            }
        }
        
        
    }

    
    void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.name == "LevelComplete" && gameObject.name == "Player")
        {
            Scores.mainScore.completedMap = true;
            Control.mainControl.isControllable = false;
            transTimer.setTimer(1);
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (gameObject.tag == "Enemy")
        {
            Attack enemyattack = collision.gameObject.GetComponent<Attack>();
            if (enemyattack != null)
            {
                Scores.mainScore.sERIOUSsCORES();
                animTimer.setTimer(0.1f);
                if (gameObject.name != "FlyingEnemy")
                {
                    renderer.material.color = Color.red;
                }
                Renderer renderKid = GetComponentInChildren<Renderer>();
                renderKid.material.color = Color.red;
                Damaged(enemyattack.Strength);

            }
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
