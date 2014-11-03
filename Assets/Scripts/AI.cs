using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	Vector3 View = new Vector3 (1f, 0f, 0f);
	public float Speed = 10.0f;
    Vector3 startPos;
    Vector3 endPos;
    float startTime;
    float progress;
    public float flySpeed;
    public float flyDist;
    Timer moveProg = new Timer();
    Timer animTimer = new Timer();
    Souls equippedSoul;
    bool isBasic;
    bool isFlying;
    bool isHeavy;

    void Start ()
    {
        equippedSoul = gameObject.GetComponent<Souls>();
        if (gameObject.name == "Enemy")
        {
            isBasic = true;
            equippedSoul.MaxHP = 3;
        }
        if (gameObject.name == "FlyingEnemy")
        {
            isFlying = true;
            equippedSoul.MaxHP = 1;
        }
        if (gameObject.name == "HeavyEnemy")
        {
            isHeavy = true;
            equippedSoul.MaxHP = 1;
        }
        equippedSoul.CurHP = equippedSoul.MaxHP;
        if (isFlying)
        {
            startPos = gameObject.transform.position;
            endPos = gameObject.transform.position;
            endPos.x += flyDist;
            startTime = Time.time;
        }
    }

	void Update ()
	{
        if (isBasic)
        { 

    		transform.Translate(View * Speed * Time.deltaTime);
    
        }
        if (isFlying)
        {
            lerpFly();
        }

        if (animTimer.Ok())
        {
            if (isFlying)
            {
                Renderer renderKid = GetComponentInChildren<Renderer>();
                renderKid.material.color = Color.white;
            }

            else
            {
                renderer.material.color = Color.white;
            }
        }

        if (equippedSoul.CurHP <= 0)
        {
            Destroy(gameObject);
            Scores.mainScore.enemiesKilled++;
        }
	}


	void OnTriggerEnter (Collider other)
	{

        if (other.tag == "EnemyBoundary" && !isFlying) 
		{
			View = -View;

				}
	}

    void OnCollisionEnter(Collision collision)
    {
            Attack enemyattack = collision.gameObject.GetComponent<Attack>();
            if (enemyattack != null)
            {
                Scores.mainScore.sERIOUSsCORES();
                animTimer.setTimer(0.1f);
                if (!isFlying)
                {
                    renderer.material.color = Color.red;
                }
                Renderer renderKid = GetComponentInChildren<Renderer>();
                renderKid.material.color = Color.red;
                Damaged(enemyattack.Strength);

            }
    }

    void Damaged(int Power)
    {
        if (equippedSoul.Defence < Power)
        { equippedSoul.CurHP -= Power - equippedSoul.Defence; }
    }

    void lerpFly ()
    {
        if (progress >= 1)
        {
            progress = 1;
        }
        if (progress == 1)
        {
            progress = 1 - progress;
            startTime = Time.time;
            float tempx = startPos.x;
            startPos.x = endPos.x;
            endPos.x = tempx;

        }
        progress = moveProg.progress(startTime, flySpeed);
        transform.position = Vector3.Lerp(startPos, endPos, progress);
          }


}
