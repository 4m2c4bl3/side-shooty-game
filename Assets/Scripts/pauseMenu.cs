using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class pauseMenu : MonoBehaviour {
    bool _menuOpen;
    bool displayMenu;
    public GUISkin menuSkin;
    string[] menu = new string[3] 
        {
            "Resume", "Main Menu", "Quit"
        };

    bool menuOpen
    {
        get
        {
            return _menuOpen;
        }

        set
        {
            if (!_menuOpen && value)
            {
                Time.timeScale = 0;
                Control.mainControl.isControllable = false;
                displayMenu = true;
            }

            _menuOpen = value;
        }
    }
	
    void OnGUI ()
    {
        if (displayMenu)
        {
            GUI.skin = menuSkin;
            GUI.BeginGroup(new Rect((Screen.width / 4), (Screen.height / 4), ((Screen.width / 4) * 2), ((Screen.height / 4) * 2)));
            GUI.Box(new Rect(0, 0, ((Screen.width / 4) * 2), ((Screen.height / 4) * 2)), "Menu");
            if (GUI.Button(new Rect(0, 25,( Screen.width * 0.5f), 50), menu[0]))
            {
                Time.timeScale = 1;
                Control.mainControl.isControllable = true;
                displayMenu = false;

            }
            if (GUI.Button(new Rect(0, 80, (Screen.width * 0.5f), 50), menu[1]))
            {
                playSound.p.Play(6);
                Application.LoadLevel("title");
                Game.current = new Game();

            }
            if (GUI.Button(new Rect(0, 135, (Screen.width * 0.5f), 50), menu[2]))
            {

                playSound.p.Play(6);
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
            }

            GUI.EndGroup();
        }
    }

	void Update () {

        menuOpen = Input.GetKey(KeyCode.Escape);

    }

}
