using UnityEngine;
using System.Collections;
using System.Linq;

[System.Serializable]
public class Control : MonoBehaviour
	
	//I did not make the player movement physics! Maxime did the entire base, and I tweaked a few things that I a) could understand and b) wanted to change.
    //If it looks complicated, it's not me.
	
{
    public bool isControllable = true;
	public Attack BasicBullet;
    Souls equippedSoul;
	//public float Shootrate = 0.5f;
	//private float Pause = 0.0f;
	public Vector3 View = new Vector3(1f, 0f, 0f);
    public bool hangYes = false;
	float hangtime = 0.35f;
	float backforce = 0.0f;
	float yforce = 0.0f;
	bool hitback = false;
	float aircontrol = 1.75f; //Change to control speed when in air
	bool _grounded = false;
    bool _defending = false;
	public Vector3 movement = Vector3.zero;
    public static Control mainControl;
    public Animator animator;

    public bool defending
    {
        get
        {
            return _defending;
        }

        set
        {
            if (value)
            {
                if (!_defending)
                {
                    isControllable = false;
                }
            }

            if (!value)
            {
                if (_defending)
                {
                  isControllable = true;
              }
            }
            _defending = value;
        }
    }

    public bool grounded
    {
        get
        {
            return _grounded;
        }
        set
        {
            if (!value)
            {
                transform.parent = null;
                Cam.mainCam.jumpin = true;
            }
            if (value)
            {
                if (!_grounded)
                {
                    Cam.mainCam.jumpin = false;
                    Cam.mainCam.resetView();
                }
            }
            _grounded = value;
        }
    }
	
	void Start()
    {
        equippedSoul = gameObject.GetComponent<Souls>();
        mainControl = this;
    }

	public void Shoot()
	{
		if (equippedSoul.Energy >= 0.0)
		{
			Vector3 SpawnPoint = transform.position + (View * 1);
            SpawnPoint.y += 0.3f;
			GameObject swing = Instantiate(BasicBullet.gameObject, SpawnPoint, transform.rotation) as GameObject;
			Attack shooted = swing.GetComponent<Attack>();
			shooted.dir = View;
			shooted.Speed = equippedSoul.Speed;
			shooted.Strength = equippedSoul.Strength;
			shooted.Shooter = gameObject;
			equippedSoul.Energy -= equippedSoul.useEnergy;
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
        rigidbody.sleepVelocity = 0;
		//Shootrate = equippedSoul.AttSpeed;
		bool ShootNow = Input.GetKeyDown(KeyCode.Space) /*&& Shootpause()*/;
		if (isControllable)
        { 
		    if (ShootNow)
		    {
			    Shoot();
		    }
		   
        }
        bool MoveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool MoveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool Jump = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool Hang = Input.GetKey(KeyCode.K);
        defending = Input.GetKey(KeyCode.L);	
        movement = Vector3.zero;

        if (!isControllable && defending)
        {

            animator.SetBool("blocking", true);

            if (MoveRight)
            {
                View = Vector3.right;
                animator.SetFloat("direction", View.x);
            }
            if (MoveLeft)
            {
                View = Vector3.left;
                animator.SetFloat("direction", View.x);
            }
        }
        if (isControllable)
        {

            animator.SetBool("blocking", false);

		    if (MoveRight)
		    {
			    movement += Vector3.right * (grounded ? 1.0f : aircontrol);
                View = Vector3.right;
                animator.SetFloat("direction", View.x);
                animator.SetBool("running", true);
                
		    }
		    if (MoveLeft)
		    {
			    movement += Vector3.left * (grounded ? 1.0f : aircontrol);
                View = Vector3.left;
                animator.SetFloat("direction", View.x);
                animator.SetBool("running", true);
			
		    }
            if (!MoveLeft && !MoveRight)
            {
                animator.SetFloat("direction", View.x);
                animator.SetBool("running", false);
            }
		    if (Jump && grounded == true)
		    {
			    yforce = 4.5f; //Intial jump force
			    grounded = false;

		    }
        }
            if (Hang && hangYes)
            {
                hangtime = 0.05f;
            }

            if (!Hang)
            {
                hangtime = 0.35f;
            }
          
		movement.y = yforce;
		
		if (hitback) 
		{

            if (backforce <= -1.5f)
            {
                //Max slowdown speed
                backforce += 0.95f; //Descent speedup rate
            }

            else if (backforce <= 0.0f)
            {
                backforce += 0.25f; //Ascent slowdown rate
            }

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

		if (collision.gameObject.tag == "Enemy" && !defending)
		{
            
			BroadcastMessage("GotHit", collision);
			hitback = true;
			backforce = 1f * -View.x;

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
}