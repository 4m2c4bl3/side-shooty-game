using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game
{
    public static Game current;
    public Scores scores; 
    public Character player; 
    public Souls psoul;
    public Control pcontrol; 

    public Game()
    {
        pcontrol = Control.mainControl;
        scores = Scores.mainScore;
        player = Character.mainChar;
        psoul = GameObject.Find("Player").gameObject.GetComponent<Souls>();
    }

}