using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
    public Control control;
    float startTime;
    float progress = 1f;
    public float moveSpeed = 0.09f;
    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 lastView;
    public Vector3 camDef;
	Vector3 camLeft;
	Vector3 camRight;
	Timer camProg = new Timer();
    public bool jumpin;
    public static Cam mainCam;
    float camOffset = 2;
    bool resetting = false;
    Timer resetProg = new Timer();
	
	void Start()
	{
        camLeft = new Vector3 (-camDef.x, camDef.y, camDef.z);
        camRight = new Vector3(camDef.x , camDef.y, camDef.z);
        startPoint = camRight;
		endPoint = camLeft;
        mainCam = this;
	}

    void jumping()
    {
        bool launch = true;

              if (launch)
              {
                  endPoint.y = camDef.y - camOffset;
                  startPoint.y = camDef.y - camOffset;
                  launch = false;
              }


      if (lastView != control.View)
      {
          progress = 1 - progress;
          startTime = Time.time;

          if (control.View == Vector3.left)
          {
              startPoint.y = camDef.y - camOffset;
              endPoint.y = camDef.y - camOffset;
              startPoint.x = camRight.x;
              endPoint.x = camLeft.x;
          }
          if (control.View == Vector3.right)
          {
              startPoint.y = camDef.y - camOffset;
              endPoint.y = camDef.y - camOffset;
              startPoint.x = camLeft.x;
              endPoint.x = camRight.x;
          }

          lastView = control.View;
      }
      progress = camProg.progress(startTime, moveSpeed);
      transform.localPosition = Vector3.Lerp(startPoint, endPoint, progress);
      if (camProg.progress(startTime, moveSpeed) >= 1)
      {
          progress = 1;
      }
        }

    void walking ()
    {
        if (lastView != control.View)
        {
            progress = 1 - progress;
            startTime = Time.time;

            if (control.View == Vector3.left)
            {
                startPoint = camRight;
                endPoint = camLeft;
            }

            if (control.View == Vector3.right)
            {
                startPoint = camLeft;
                endPoint = camRight;
            }

            lastView = control.View;
        }
        progress = camProg.progress(startTime, moveSpeed);
        transform.localPosition = Vector3.Lerp(startPoint, endPoint, progress);
        if (camProg.progress(startTime, moveSpeed) >= 1)
        {
            progress = 1;
        }
    }
        
 public void resetView ()
  {
      resetting = true;
      progress = 1 - progress;
      startTime = Time.time;
      if (control.View == Vector3.left)
      {
          startPoint.x = camLeft.x;
          endPoint = camLeft;
      }
      if (control.View == Vector3.right)
      {
          startPoint.x = camRight.x;
          endPoint = camRight;
      }
      progress = camProg.progress(startTime, moveSpeed); //i dont think this is doing anything at all. it's using movespeed not resetspeed, so clearly walking is being used. how i fix?
      transform.localPosition = Vector3.Lerp(startPoint,endPoint,progress);
      resetProg.setTimer(progress);
      resetting = false;
     if (progress >= 1)
     {
         progress = 1;
         
     }

  
  }

    void Update()
    {
        if (jumpin == true)
        {
            jumping();
        }
        if (jumpin == false)
        {
            if (resetting == false)
            {
                walking();
            }
            if (resetProg.Ok())
            { 
                resetProg.sleep();

            }
           
                            
        }
       

		
    }
}
