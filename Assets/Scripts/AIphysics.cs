using UnityEngine;
using System.Collections;
using System.Linq;

public class AIphysics : MonoBehaviour {

    float backforce = 0.0f;
    float yforce = 0.0f;
    public bool _hitback = false; 
    bool grounded = false;
    public Vector3 movement = Vector3.zero;
    public AI thisAI;

    
    bool hitback
    {
        get 
        {
            return _hitback;
        }

        set
        {
            _hitback = value;
        }
    }

    void Update ()
    {
        movement = Vector3.zero;

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
            else if (yforce > -1.5f) //Max slowdown speed
            {
                yforce -= 0.75f; //Descent speedup rate
            }
        }

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

        if (collision.gameObject.tag == "Player" && Control.mainControl.defending)
        {

            hitback = true;
            backforce = 1f * -thisAI.View.x;

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
