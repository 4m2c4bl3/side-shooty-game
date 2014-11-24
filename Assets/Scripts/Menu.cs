using UnityEngine;
using UnityEditor;
using System.Collections;

public class Menu : MonoBehaviour {

    GameObject controlInfo;
    public GameObject bgm;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("BGM") == null)
        {
            GameObject BGMusic = Instantiate(bgm.gameObject, transform.position, transform.rotation) as GameObject;
        }

        controlInfo = GameObject.FindGameObjectWithTag("ControlInfo");

        if (gameObject.name == "FinalScore")
        {
            if (Scores.mainScore.completedMap == true)
            {
                guiText.text = "You Won! \n You killed " + Scores.mainScore.enemiesKilled + " enemies.\n You died " + Scores.mainScore.livesLost + " times.";
            }
            if (Scores.mainScore.completedMap == false)
            {
                guiText.text = "You Lost. \n You killed " + Scores.mainScore.enemiesKilled + " enemies.\n You died " + Scores.mainScore.livesLost + " times.";
            }

        }
    }

    void OnMouseUpAsButton()
    {
        if (gameObject.name == "Start" || gameObject.name == "Reload")
        {
            playSound.p.Play(6);
            if (GameObject.FindGameObjectWithTag("Score") != null)
            {
                GameObject.Destroy(GameObject.FindGameObjectWithTag("Score"));
            }
            Application.LoadLevel("game");
            Game.current = new Game();
        }

        //for continue
        //saveGame.Load();


        if (gameObject.name == "Quit")
        {
            playSound.p.Play(6);
            Application.Quit();
            EditorApplication.isPlaying = false;
        }
    }

    void OnMouseOver()
    {
        if (gameObject.name == "Controls")
        {
            controlInfo.guiText.text = "Use WASD or arrow keys to navigate. \nA and D or Left and Right to move,\nW and Up to jump. \nUse Space Bar To Shoot.";
        }
    }
    void OnMouseExit()
    {
        if (gameObject.name == "Controls")
        {
            controlInfo.guiText.text = "";
        }
    }
	
}