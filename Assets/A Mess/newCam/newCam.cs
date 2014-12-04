using UnityEngine;
using System.Collections;

public class newCam : MonoBehaviour {

   public static newCam main;
   public Vector3 ceiling;
   public Vector3 floor;
   float ceilingOffset = 2f;
   Vector3 toView = new Vector3(0, 1, 0);
   Timer holdTimer = new Timer();
   float camx;

    void Awake()
   {
       main = this;
   }

    void Start ()
    {
        ceiling = gameObject.transform.position;
        ceiling.y = newcamChar.main.gameObject.transform.position.y + ceilingOffset;
        floor = gameObject.transform.position;
        floor.y = newcamChar.main.gameObject.transform.position.y;
        camx = gameObject.transform.position.x;
        
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(ceiling, new Vector3(5, 0.5f, 1));

        Gizmos.color = new Color(0, 1, 0, 0.5F);
        Gizmos.DrawCube(floor, new Vector3(5, 0.5f, 1));
    }

    public void reset (Vector3 playerpos)
    {
        ceiling = new Vector3(gameObject.transform.position.x, (playerpos.y + ceilingOffset), 0);
        floor = new Vector3(gameObject.transform.position.x, (playerpos.y - 1), 0);
    }

    void Update ()
    {

        floor = new Vector3(gameObject.transform.position.x, (newcamChar.main.gameObject.transform.position.y - 1), 0);
        float playerx = newcamChar.main.gameObject.transform.position.x;
        

        //if (newcamChar.main.moveLeft)
        //{
        //    toView.y = newcamChar.main.gameObject.transform.position.y;       
        //    toView.x = (-camx + -1);
        //    transform.Translate(toView * 0.5f * Time.deltaTime);
        //}
//
        //if (newcamChar.main.moveRight)
        //{
       //     toView.y = newcamChar.main.gameObject.transform.position.y;     
       //     toView.x = (camx + 1);
       //     transform.Translate(toView * 0.5f * Time.deltaTime);
       // }


        if (newcamChar.main.gameObject.transform.position.y > ceiling.y)
        {
            toView.y = newcamChar.main.gameObject.transform.position.y;
            toView.z = 0;
            transform.Translate(toView * 0.5f * Time.deltaTime);
        }
            
         else
        {
            if (gameObject.transform.position.y > (floor.y + 6))
            {
                toView.y = newcamChar.main.gameObject.transform.position.y;
                toView.z = 0;
                transform.Translate(floor * 1 * Time.deltaTime);
         }

            }
        }
}