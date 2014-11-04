using UnityEngine;
using System.Collections;

public class newcamChar : MonoBehaviour
{
    Vector3 view = new Vector3(1, 0, 0);
    Vector3 viewFront = new Vector3(1, 0, 0);
    float speed = 10.0f;
    float jumpHeight = 20.0f;
    bool _grounded;
    public static newcamChar main;

    public bool grounded
    {
        get
        {
            return _grounded;
        }
        set
        {
            if (value)
            {
                if (!_grounded)
                {
                    newCam.main.reset(gameObject.transform.position);
                }
            }

            _grounded = value;
        }
    }


    void Start()
    {
        main = this;
    }
    void Update()
    {

        bool jump = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (moveLeft)
        {
            transform.Translate(view * speed * Time.deltaTime, Space.World);

            if (view == viewFront)
            {
                view = -viewFront;
                transform.Rotate(new Vector3(0, -180, 0));
            }

        }
        if (moveRight)
        {
            transform.Translate(view * speed * Time.deltaTime, Space.World);

            if (view != viewFront)
            {
                view = viewFront;
                transform.Rotate(new Vector3(0, -180, 0));
            }

        }
        if (jump)
        {
            if (grounded)
            {
                Vector3 viewUP = new Vector3(view.x, view.y + jumpHeight, view.z);
                transform.Translate(viewUP * speed * Time.deltaTime);
            }
        }
    }

   
}
