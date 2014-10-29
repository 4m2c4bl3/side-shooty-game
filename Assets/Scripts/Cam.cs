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
	Vector3 Camleft = new Vector3(-3.5f, 4.6f, -6f);
	Vector3 Camright = new Vector3(3.5f, 4.6f, -6f);
	Timer camProg = new Timer();
	
	//Camera.main.transform.localPosition = Camleft;
    // Camera.main.transform.localPosition = Camright;

	void Start()
	{
		startPoint = Camright;
		endPoint = Camleft;
	}


    void Update()
    {
		
		if (lastView != control.View)
        {
            progress = 1 - progress;
            startTime = Time.time;

             if (control.View == Vector3.left)
            {
				startPoint.x = Camright.x;
				endPoint.x = Camleft.x;
                
            }
            if (control.View == Vector3.right)
             {
				startPoint.x = Camleft.x;
				endPoint.x = Camright.x;
                
             }
              
                lastView = control.View;
        }
		progress = camProg.progress (startTime, moveSpeed);
		transform.localPosition = Vector3.Lerp(startPoint, endPoint, progress);
		if (camProg.progress(startTime, moveSpeed) >= 1)
			if (progress >= 1)
		{
			progress = 1;
		}
    }
}
