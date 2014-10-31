using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float MaxHP = 5f;
    public float CurHP = 0f;
    public Souls EquippedSoul;
    public float lives = 3;
    public Vector3 startPos;
    Timer animTimer = new Timer();

    // Use this for initialization
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
                lives -= 1;
                Scores.mainScore.livesLost ++;
                CurHP = MaxHP;
                transform.position = startPos;
                gameObject.GetComponent<Control>().movement = Vector3.zero;

                if (lives <= 0)
                {
                    Application.LoadLevel("over");
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
            Application.LoadLevel("over");
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
