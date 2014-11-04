using UnityEngine;
using System.Collections;

[System.Serializable]
public class Scores : MonoBehaviour {
   public float livesLost = 0;
   public float enemiesKilled = 0;
   public bool completedMap = false;
   public float powerUps = 0;
   int howgoodamiscoring = 1;
   public static Scores mainScore;
  
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        mainScore = this;
    }

    public void sERIOUSsCORES() //i hope you enjoy my fully functioning and terribly important score system. thank you!
    {
        howgoodamiscoring = Random.Range(1, 5);

    }

    void OnGUI()
    {
        switch (howgoodamiscoring)
        {
            case 1:
                GUI.Label(new Rect((Screen.width / 2) - 150, Screen.height - 55, 200, 55), "<size=20>SCORE: HORRIBLE</size>");
                break;
            case 2:
                GUI.Label(new Rect((Screen.width / 2) - 150, Screen.height - 55, 200, 55), "<size=20>SCORE: MEDIOCRE</size>");
                break;
           case 3:
                GUI.Label(new Rect((Screen.width / 2) - 150, Screen.height - 55, 200, 55), "<size=20>SCORE: INCREDIBLE</size>");
                break;
           case 4:
                GUI.Label(new Rect((Screen.width / 2) - 150, Screen.height - 55, 200, 55), "<size=20>SCORE: GOD-LIKE</size>");
                break;
           case 5:
                GUI.Label(new Rect((Screen.width / 2) - 150, Screen.height - 55, 200, 55), "<size=20>SCORE: OK</size>");
                break;


        }
    }

}
