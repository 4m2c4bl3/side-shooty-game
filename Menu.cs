using UnityEngine;
using UnityEditor;
using System.Collections;

public class Menu : MonoBehaviour {
  
    void OnMouseUpAsButton()
    {
        if (gameObject.name == "Start")
        {
            Application.LoadLevel("game");
        }

        if (gameObject.name == "Quit")
        {
            Application.Quit();
            EditorApplication.isPlaying = false;
        }
    }

    void OnMouseOver()
    {
        if (gameObject.name == "Controls")
        {
            var controlInfo = GameObject.FindGameObjectWithTag("ControlInfo");
            controlInfo.guiText.text = "Use WASD or arrow keys to navigate. \nA and D or Left and Right to move,\nW and Up to jump. \nUse Space Bar To Shoot.";
        }
    }
    void OnMouseExit()
    {
        if (gameObject.name == "Controls")
        {
            var controlInfo = GameObject.FindGameObjectWithTag("ControlInfo");
            controlInfo.guiText.text = "";
        }
    }
}
