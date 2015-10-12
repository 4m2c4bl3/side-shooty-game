using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	Vector3 _view = new Vector3 (1f, 0f, 0f);
	public float Speed = 10.0f;
    Vector3 startPos;
    Vector3 endPos;
    float startTime;
    float progress;
    public float flySpeed;
    public float flyDist;
    Timer moveProg = new Timer();
    Timer animTimer = new Timer();
    Timer shootTimer = new Timer();
    Souls equippedSoul;
    bool isBasic;
    bool isFlying;
    bool isHeavy;
    bool isAngry;
    bool isAttacking;
    public float attSpeed = 1.75f;
    public Attack BasicBullet;
    Vector3 shootDir = new Vector3(0, 0, 0);
    bool isLeft;
    float rufRotate;

    public Vector3 View
    {

        get
        {
            return _view;
        }

        set
        {
            if (_view != value)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                rufRotate = Mathf.Round(transform.rotation.y);
                if (transform.rotation.y != 180)
                {
                    isLeft = true;
                }
                if (rufRotate == 0)
                {
                    isLeft = false;
                }
            }

            _view = value;
        }

    }

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
            Heavy();
        }

        if (gameObject.name == "AngryEnemy")
        {
            isAngry = true;
            equippedSoul.MaxHP = 1;
            equippedSoul.Speed = 20;

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
    void Heavy ()
    {
            GameObject shield = GameObject.CreatePrimitive(PrimitiveType.Cube);
            shield.transform.position = new Vector3(transform.position.x + View.x, transform.position.y, transform.position.z);
            shield.GetComponent<Collider>().enabled = false; 
            shield.transform.parent = gameObject.transform;
    }

    void Update()
    {
        if (equippedSoul.CurHP <= 0)
        {
            if (!isFlying)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {

                Renderer renderKid = GetComponentInChildren<Renderer>();
                renderKid.material.color = Color.red;
            }

            Destroy(gameObject);
            Scores.mainScore.enemiesKilled++;
        }
        if (!isFlying && !gameObject.GetComponent<AIphysics>().hitback)
        {
            if (!isAttacking)
            {
                transform.Translate(View * Speed * Time.deltaTime, Space.World);
            }

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
                    GetComponent<Renderer>().material.color = Color.white;
                }
            }

        if (isAngry)
        {
            float playerPos = Mathf.Round(Character.mainChar.gameObject.transform.position.y);
            float myPos = Mathf.Round(transform.position.y);
            if (myPos == playerPos)
            {
                print(shootDir.x);

                isAttacking = true;
                if (!shootTimer.inuse)
                {
                    shootTimer.setTimer(attSpeed);
                }
                print("smp");
                shootDir.x = Character.mainChar.gameObject.transform.position.x;
                             

                if (isLeft)
                {
                    if (Character.mainChar.gameObject.transform.position.x < gameObject.transform.position.x)
                    {
                        shootDir.x = 1.0f;
                    }

                    if (Character.mainChar.gameObject.transform.position.x > gameObject.transform.position.x)
                    {
                        shootDir.x = -1.0f;
                    }
                }

                if (!isLeft)
                {
                    if (Character.mainChar.gameObject.transform.position.x < gameObject.transform.position.x)
                    {
                        shootDir.x = -1.0f;
                    }

                    if (Character.mainChar.gameObject.transform.position.x > gameObject.transform.position.x)
                    {
                        shootDir.x = 1.0f;
                    }
                }


            } 
            else
            {                
                isAttacking = false;
            }

            if (shootTimer.Ok())
            {
                print("wao");
                playSound.p.Play(0);
                Vector3 SpawnPoint = transform.position + (View * 1);
                SpawnPoint.y += 0.2f;
                GameObject swing = Instantiate(BasicBullet.gameObject, SpawnPoint, transform.rotation) as GameObject;
                Attack shooted = swing.GetComponent<Attack>();
                shooted.dir = shootDir;
                shooted.Speed = equippedSoul.Speed;
                shooted.Strength = equippedSoul.Strength;
                shooted.Shooter = gameObject;
                equippedSoul.Energy -= equippedSoul.useEnergy;
                Physics.IgnoreCollision(shooted.GetComponent<Collider>(), GetComponent<Collider>());
                shootTimer.sleep();

            }
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

                if (isFlying)
                {
                    Damaged(enemyattack.Strength);
                    Renderer renderKid = GetComponentInChildren<Renderer>();
                    renderKid.material.color = Color.red;
                    animTimer.setTimer(0.1f);
                }
                
                
                if (isHeavy)
                {
                    if (View == Control.mainControl.View)
                    {
                        Damaged(enemyattack.Strength);
                        GetComponent<Renderer>().material.color = Color.red;
                        animTimer.setTimer(0.1f);
                    }
                }
                
                if (isBasic || isAngry)
                {
                    Damaged(enemyattack.Strength);
                    GetComponent<Renderer>().material.color = Color.red;
                    animTimer.setTimer(0.1f);
                }

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
