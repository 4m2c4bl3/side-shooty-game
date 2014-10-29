using UnityEngine;
using System.Collections;
using System.Linq;

public class Control : MonoBehaviour
	
	//I did not make the physics! Everything I did not do is in the 'by maxime' areas. If you guess who helped me with them, you win the game.
    //
	
{
	public Attack BasicBullet;
	public Souls EquippedSoul;
	//public float Shootrate = 0.5f;
	//private float Pause = 0.0f;
	public Vector3 View = new Vector3(1f, 0f, 0f);
	public float hangtime = 0.35f;
	
	//by maxime start
	float backforce = 0.0f;
	float yforce = 0.0f;
	bool hitback = false;
	float aircontrol = 1.75f; //Change to control speed when in air
	public bool grounded = false;
	Vector3 movement = Vector3.zero;
	//by maxime end
	
	
	public void Shoot()
	{
		if (EquippedSoul.Energy >= 0.0)
		{
			Vector3 SpawnPoint = transform.position + (View * 1);
			GameObject swing = Instantiate(BasicBullet.gameObject, SpawnPoint, transform.rotation) as GameObject;
			Attack shooted = swing.GetComponent<Attack>();
			shooted.dir = View;
			shooted.Speed = EquippedSoul.Speed;
			shooted.Strength = EquippedSoul.Strength;
			shooted.Shooter = gameObject;
			EquippedSoul.Energy -= EquippedSoul.useEnergy;
			Physics.IgnoreCollision(shooted.collider, collider);
		}
		
	}
	
	
	//public bool Shootpause()
	//{
	
	
	//if (Time.time >= Pause)
	//{
	//	Pause = Time.time + Shootrate;
	//	return true;
	//}
	
	//	return false;
	//}	
	
	void Update()
	{
		//Shootrate = EquippedSoul.AttSpeed;
		bool ShootNow = Input.GetKeyDown(KeyCode.Space) /*&& Shootpause()*/;
		
		if (ShootNow)
		{
			Shoot();
		}
		bool MoveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		bool MoveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
		bool Jump = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
		
		//by maxime start
		
		//Needs to be done in Update because keyboards loves update
		movement = Vector3.zero;
		if (MoveRight)
		{
			movement += Vector3.right * (grounded ? 1.0f : aircontrol);
			View = Vector3.right; //notbymaxime
			
		}
		if (MoveLeft)
		{
			movement += Vector3.left * (grounded ? 1.0f : aircontrol);
			View = Vector3.left;//notbymaxime
			
		}
		if (Jump && grounded == true)
		{
			yforce = 4.5f; //Intial jump force
			grounded = false;
            transform.parent = null;

		}
		
		movement.y = yforce;
		
		if (hitback) 
		{
			movement.x = backforce; 
			
		}

        if (grounded && transform.parent != null)
        {
            TestGround(transform.parent.collider);
        }
		
		if (!grounded)
		{
			if (yforce > 0.0f)
			{
				yforce -= 0.35f; //Ascent slowdown rate
			}
			else if (yforce < 0.7f && yforce > -0.7f)
			{
				yforce -= hangtime;
			}
			else if (yforce > -1.5f) //Max slowdown speed
			{
				yforce -= 0.75f; //Descent speedup rate
			}
		}
	}
	
	void FixedUpdate()
	{
		//Needs to be done in fixed update because rigidbodies digs it
		rigidbody.velocity = Vector3.zero;
		rigidbody.MovePosition(rigidbody.position + (movement * 10.0f * Time.deltaTime));
	}
	
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
            if (collision.contacts.All(x => x.normal == Vector3.down)) // MOAR MAJICKS
            {
                yforce = 0.0f;
            }
		}
		
		if (collision.gameObject.name == "Enemy" && gameObject.name == "Player")
		{
			BroadcastMessage("GotHit", collision);
			hitback = true;
			backforce = 1f * -View.x;
			
			if (backforce > 0.0f)
			{
				backforce -= 0.35f; //Ascent slowdown rate
			}
			else if (backforce > -1.5f)
			{ 
				//Max slowdown speed
				backforce -= 0.75f; //Descent speedup rate
			}
		}
	}

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && grounded == false)
        {
            if (collision.contacts.All(x => x.normal == Vector3.up))
            {
                yforce = 0.0f;
                grounded = true;
                transform.parent = collision.gameObject.transform;
                if (hitback)
                {
                    hitback = false;
                }
            }
        }
    }
	
	void OnCollisionExit(Collision collision)
	{
        if (collision == null)
        {
            grounded = false;
            transform.parent = null;
        }
		else if (collision.gameObject.tag == "Ground")
		{
            TestGround(collision.collider);
		}
	}

    void TestGround(Collider col)
    {
        RaycastHit rayhit;
        Physics.Raycast(transform.position, Vector3.down, out rayhit, collider.bounds.extents.y + 0.4f, 1 << 10 | 1 << 11);
        if (rayhit.collider == null || rayhit.collider != col)
        {
            grounded = false;
            transform.parent = null;
        }
    }
    //by maxime end
}