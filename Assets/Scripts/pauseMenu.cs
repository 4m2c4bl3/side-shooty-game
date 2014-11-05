using UnityEngine;
using UnityEditor;
using System.Collections;

public class pauseMenu : MonoBehaviour {
    public bool menuOpen;
    string[] menu = new string[2] 
        {
            "Resume", "Quit"
        };

    int selected;
    //string menu3 = "Save";

    //SaveLoad.Save();

    //for quit
    //Application.LoadLevel("menu");

    /*int menuSelection(string[] menuArray, int selectedItem, string direction)
    {

        if (direction == "up")
        {

            if (selectedItem == 0)
            {

                selectedItem = menuArray.length - 1;

            }
            else
            {

                selectedItem -= 1;

            }

        }

        if (direction == "down")
        {

            if (selectedItem == menuArray.length - 1)
            {

                selectedItem = 0;

            }
            else
            {

                selectedItem += 1;

            }

        }

        return selectedItem;

    }

	void Start () {
        menuOpen = false;
        selected = 0;
	}
	
    void OnGUI ()
    {
        if (menuOpen)
        {            
            GUI.BeginGroup(new Rect((Screen.width / 4), (Screen.height / 4), ((Screen.width / 4) * 2), ((Screen.height / 4) * 2)));
            GUI.Box(new Rect(0, 0, ((Screen.width / 4) * 2), ((Screen.height / 4) * 2)), "Menu");
            GUI.SetNextControlName(menu[0]);
            if (GUI.Button(new Rect(0, 0, 50,50), menu[0]))
            {

            }
            GUI.SetNextControlName(menu[1]);
            if (GUI.Button(new Rect(0, 0, 50,50), menu[1]))
            {

            }

            GUI.FocusControl(menu[selected]);
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
         menuUp = Input.GetKeyDown(KeyCode.W);
         menuDown = Input.GetKeyDown(KeyCode.S);
         menuSelect = Input.GetKeyDown(KeyCode.Space);

         if (menuUp)
         {
             selected = menuSelection(menu, selected, "up");
         }

         if (menuDown)
         {
             selected = menuSelection(menu, selected, "down");
         } 
     }
        else
     {
         Time.timeScale = 1;
     }
	}*/

}
