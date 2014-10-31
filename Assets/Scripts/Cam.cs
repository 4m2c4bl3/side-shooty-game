using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
    public Control control;
    float startTime;
    float progress = 1f;
    public float moveSpeed = 0.2f;	
    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 lastView;
    public Vector3 camDef;
	Vector3 camLeft;
	Vector3 camRight;
	Timer camProg = new Timer();
    public bool jumpin;
    public static Cam mainCam;
	
	//Camera.main.transform.localPosition = Camleft;
    // Camera.main.transform.localPosition = Camright;

	void Start()
	{
        camLeft = new Vector3 (-camDef.x, camDef.y, camDef.z);
        camRight = new Vector3 (camDef.x, camDef.y, camDef.z);
        startPoint = camRight;
		endPoint = camLeft;
        mainCam = this;
	}

    void jumping()
    {
        bool launch = true;

              if (launch)
              {
                  endPoint.y = 4.6f - 3;
                  startPoint.y = 4.6f - 3;
                  launch = false;
              }


      if (lastView != control.View)
      {
          progress = 1 - progress;
          startTime = Time.time;

          if (control.View == Vector3.left)
          {
              startPoint.x = camRight.x;
              endPoint.x = camLeft.x;
              startPoint.y = 4.6f;
              endPoint.y = 4.6f;

          }
          if (control.View == Vector3.right)
          {
              startPoint.x = camLeft.x;
              endPoint.x = camRight.x;
              startPoint.y = 4.6f;
              endPoint.y = 4.6f;

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
      progress = 1 - progress;
      startTime = Time.time;
      startPoint = Camera.main.transform.localPosition;
      endPoint = startPoint;
      endPoint.y = 4.6f;
      progress = camProg.progress(startTime, moveSpeed);
      transform.localPosition = Vector3.Lerp(startPoint,endPoint,progress);
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

		if (lastView != control.View)
        {
            progress = 1 - progress;
            startTime = Time.time;

             if (control.View == Vector3.left)
            {
				startPoint.x = camRight.x;
				endPoint.x = camLeft.x;
            }

            if (control.View == Vector3.right)
             {
				startPoint.x = camLeft.x;
				endPoint.x = camRight.x;
             }
              
                lastView = control.View;
        }
		progress = camProg.progress (startTime, moveSpeed);
		transform.localPosition = Vector3.Lerp(startPoint, endPoint, progress);
        if (camProg.progress(startTime, moveSpeed) >= 1)
		{
			progress = 1;
		}
    }
}
