using UnityEngine;
using UnityEditor;
using System.Collections;

public class pauseMenu : MonoBehaviour {
    public bool menuOpen;
    string menu1 = "Resume";
    string menu2 = "Quit";
    //string menu3 = "Save";

    //SaveLoad.Save();

    //for quit
    //Application.LoadLevel("menu");

	void Start () {
        menuOpen = false;
	}
	
    void OnGUI ()
    {
        if (menuOpen)
        {
            GUI.BeginGroup(new Rect((Screen.width / 4), (Screen.height / 4), ((Screen.width / 4) * 2), ((Screen.height / 4) * 2)));
            GUI.Box(new Rect(0, 0, ((Screen.width / 4) * 2), ((Screen.height / 4) * 2)), "Menu");
            GUI.EndGroup();
        }
    }

	void Update () {
        bool menuUp;
        bool menuDown;
        bool menuSelect;
	 if (menuOpen)
     {
         Time.timeScale = 0;
         Control.mainControl.isControllable = false;
         menuUp = Input.GetKey(KeyCode.W);
         menuDown = Input.GetKey(KeyCode.S);
         menuSelect = Input.GetKey(KeyCode.Space);
     }
        else
     {
         Time.timeScale = 1;
     }
	}
}
