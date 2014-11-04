using UnityEngine;
using System.Collections;

public class newCam : MonoBehaviour {

   public static newCam main;
   public Vector3 ceiling;
   public Vector3 floor;
   float ceilingOffset = 4f;
   Vector3 leftView = new Vector3(-1, 0, 0);
   Vector3 rightView = new Vector3(1, 0, 0);
   Vector3 upView = new Vector3(0, 1, 0);
   Vector3 middleView = new Vector3(0, 0, 0);

    void Start ()
    {
        main = this;
        ceiling = gameObject.transform.position;
        ceiling.y = newcamChar.main.gameObject.transform.position.y + ceilingOffset;
        floor = gameObject.transform.position;
        floor.y = newcamChar.main.gameObject.transform.position.y;
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
        ceiling = new Vector3(gameObject.transform.position.x, (playerpos.y + ceilingOffset), gameObject.transform.position.x);
        floor = new Vector3(gameObject.transform.position.x, (playerpos.y - 1), gameObject.transform.position.x);
    }

    void Update ()
    {

        floor = new Vector3(gameObject.transform.position.x, (newcamChar.main.gameObject.transform.position.y - 1), gameObject.transform.position.x);

        if (newcamChar.main.gameObject.transform.position.y > ceiling.y)
        {
            upView = new Vector3 (0, newcamChar.main.gameObject.transform.position.y, 0);
            transform.Translate(upView * 0.5f * Time.deltaTime);
        }
            

        else
        {
            if (gameObject.transform.position.y > (floor.y + 6))
            {
                upView = new Vector3(0, newcamChar.main.gameObject.transform.position.y, 0);
                transform.Translate(floor * 1 * Time.deltaTime);

            }
        }
    }
}
