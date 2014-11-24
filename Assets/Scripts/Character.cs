using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character : MonoBehaviour {

    Souls equippedSoul;
    public float lives = 3;
    public Vector3 startPos;
    public static bool _respawn;
    Timer animTimer = new Timer();
    Timer transTimer = new Timer();
    public static Character mainChar;

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
                    Control.mainControl.Dead(true);
                    playSound.p.Play(7);
                }
                if (transTimer.Ok())
                {
                    lives -= 1;
                    Scores.mainScore.livesLost++;
                    equippedSoul.CurHP = equippedSoul.MaxHP;
                    gameObject.GetComponent<Control>().movement = Vector3.zero;
                    transform.position = startPos;
                    Control.mainControl.isControllable = true;
                    Control.mainControl.Dead(false);
                    transTimer.sleep();
                    respawn = false;
                }
            }
            _respawn = value;
        }
    }

    void Start()
    {

        DontDestroyOnLoad(transform.gameObject); 
        equippedSoul = gameObject.GetComponent<Souls>();
        equippedSoul.CurHP = equippedSoul.MaxHP;
        startPos = transform.position;
        mainChar = this;
    }


    void Damaged(int Power)
    {
        if (equippedSoul.Defence < Power && !Control.mainControl.defending)
        {
            playSound.p.Play(1);
            equippedSoul.CurHP -= Power - equippedSoul.Defence; 
        }
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
                    playSound.p.Play(6);
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
            playSound.p.Play(6);
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
        if (collision.gameObject.tag == "Enemy")
        {
            Damaged(collision.gameObject.GetComponent<Souls>().Strength);
            animTimer.setTimer(0.1f);
            Renderer renderKid = GetComponentInChildren<Renderer>();
            renderKid.material.color = Color.yellow;
        }

    }

}
