using UnityEngine;
using System.Collections;
using System.Linq;

public class ControlNEW : MonoBehaviour

//I did not make the physics! Everything I did not do is in the 'by maxime' areas. If you guess who helped me with them, you win the game.
//I made all stuff relating to hitback though, refering to how he did the jumping. :3c
{
    public Attack BasicBullet;
    public Souls EquippedSoul;
    //public float Shootrate = 0.5f;
    //private float Pause = 0.0f;
    public Vector3 View = new Vector3(1f, 0f, 0f);

    //by maxime start
    float backforce = 0.0f;
    float yforce = 0.0f;
    bool hitback = false;
    float aircontrol = 1.75f; //Change to control speed when in air
    public bool grounded = false;
    Vector3 movement = Vector3.zero;
    GameObject groundedOn;
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
            groundedOn = null;
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
                yforce -= 0.05f;
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
        if (groundedOn != null)
        {
            float newY = groundedOn.transform.position.y + groundedOn.collider.bounds.extents.y + collider.bounds.extents.y;
            rigidbody.position = new Vector3(rigidbody.position.x, newY, rigidbody.position.z);
        }
        else
        {
            grounded = false;
        }
    }
    //by maxime end

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (collision.contacts.All(x => x.normal == Vector3.up)) // MAJICKS
            {
                yforce = 0.0f;
                grounded = true;
                groundedOn = collision.gameObject;
                if (hitback)
                {
                    hitback = false;
                }
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

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            RaycastHit rayhit;
            Physics.Raycast(transform.position, Vector3.down, out rayhit, collider.bounds.extents.y + 0.4f);
            if (rayhit.collider == null || rayhit.collider != collision.collider)
            {
                grounded = false;
                groundedOn = null;
            }
        }
    }
}

