using UnityEngine;
using UnityEditor;
using System.Collections;

public class Menu : MonoBehaviour {
  
    void OnMouseUpAsButton()
    {
        if (gameObject.name == "Start" || gameObject.name == "Reload")
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
	void Start ()
	{
		if (gameObject.name == "FinalScore")
		{
			var scoreData = GameObject.FindGameObjectWithTag("Score");
			if (scoreData.gameObject.GetComponent<Scores>().completedMap == true)
			{
				guiText.text = "You Won! \n You killed " + scoreData.gameObject.GetComponent<Scores>().enemiesKilled + " enemies.\n You died " + scoreData.gameObject.GetComponent<Scores>().livesLost + " times.";
			}
			if (scoreData.gameObject.GetComponent<Scores>().completedMap == false)
			{
				guiText.text = "You Lost. \n You killed " + scoreData.gameObject.GetComponent<Scores>().enemiesKilled + " enemies.\n You died " + scoreData.gameObject.GetComponent<Scores>().livesLost + " times.";
			}

		}
}
}