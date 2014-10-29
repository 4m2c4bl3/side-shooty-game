using UnityEngine;
using System.Collections;

public class ControlOLD : MonoBehaviour 

	//I did not make the physics! Everything I did not do is in the 'by maxime' areas. If you guess who helped me with them, you win the game.
	//I made all stuff relating to hitback though, refering to how he did the jumping. :3c
{
	public Attack BasicBullet;
	public Souls EquippedSoul;
	//public float Shootrate = 0.5f;
	//private float Pause = 0.0f;
	public Vector3 View = new Vector3 (1f, 0f, 0f);
    public float hangtime = 0.35f;
	
	//by maxime start
	public BoxCollider GroundCollider; //The collider that checks if on ground
	float backforce = 0.0f;
	float yforce = 0.0f;
	bool hitback = false;
	float aircontrol = 1.75f; //Change to control speed when in air
	public bool grounded = false;
	Vector3 movement = Vector3.zero;
	//by maxime end
	
	
	public void Shoot ()
	{
		if (EquippedSoul.Energy >= 0.0) 
		{
                        Vector3 transTemp = transform.position;
						Vector3 SpawnPoint = new Vector3((transTemp.x + (View.x * 1)), transTemp.y + 0.3f, transTemp.z);
						GameObject swing = Instantiate (BasicBullet.gameObject, SpawnPoint, transform.rotation) as GameObject;
						Attack shooted = swing.GetComponent<Attack> ();
						shooted.dir = View;
						shooted.Speed = EquippedSoul.Speed;
						shooted.Strength = EquippedSoul.Strength;
						shooted.Shooter = gameObject;
						EquippedSoul.Energy -= EquippedSoul.useEnergy;
						Physics.IgnoreCollision (shooted.collider, collider);
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
		}
		
		movement.y = yforce;
			
		if (hitback) //notbymaxime
		{
			movement.x = backforce; //not..you get the point

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
		//by maxime end

        
	}
	
	void OnCollisionEnter(Collision collision)
	{
        
		BroadcastMessage("GotHit", collision);

		if (collision.gameObject.name == "Enemy" && gameObject.name == "Player") {
			hitback = true;
			backforce = 1f * -View.x;

			if (backforce > 0.0f) {
				backforce -= 0.35f; //Ascent slowdown rate
			} 
			else if (backforce > -1.5f) { //Max slowdown speed
				backforce -= 0.75f; //Descent speedup rate
			 

						}
				}
	}
	
	//by maxime start
	void FixedUpdate () 
	{
		//Needs to be done in fixed update because rigidbodies digs it
		rigidbody.velocity = Vector3.zero;
		transform.Translate(movement * 10.0f * Time.fixedDeltaTime);
	}
	void OnTriggerEnter(Collider other)
	{
        
		if (other.tag == "Ground")
		{
			yforce = 0.0f;
			grounded = true;

			if (hitback)
			{
				hitback = false;
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Ground")
		{
			grounded = false;
		}
	}
	//by maxime end
}
