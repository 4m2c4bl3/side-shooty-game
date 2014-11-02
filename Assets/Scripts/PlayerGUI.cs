using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	public Texture2D powerBoarder;
	public Texture2D powerMeasure;
	public Texture2D healthBorder;
	public Texture2D healthMeasure;
    public Texture2D healthIcon;
    public Texture2D powerIcon;
    public Texture2D livesIcon;
	public float power = 1.0f;
	public float life = 1.0f;
	public GameObject player;
	
    void OnGUI()
    {
        GUI.Label(new Rect(1, 400, 55, 55), powerIcon);
		GUI.Label (new Rect (1,0,64,400), powerBoarder);
		GUI.BeginGroup (new Rect (1,(1 + (388-power)),52,(power)));
		GUI.Label (new Rect (0,0,52,400), powerMeasure);
		GUI.EndGroup ();

        GUI.Label(new Rect((Screen.width - 50), 400, 55, 55), healthIcon);
        GUI.Label(new Rect((Screen.width - 50), 0, 64, 400), healthBorder);
		GUI.BeginGroup (new Rect ((Screen.width-50),(1 + (388-life)),52,(life)));
		GUI.Label (new Rect (0,0,52,400), healthMeasure);
		GUI.EndGroup ();

        if (player.gameObject.GetComponent<Character>().lives == 3)
        {
            GUI.Label(new Rect((Screen.width / 2) - 55, 0, 55, 55), livesIcon);
            GUI.Label(new Rect((Screen.width / 2), 0, 55, 55), livesIcon);
            GUI.Label(new Rect((Screen.width / 2) + 55, 0, 55, 55), livesIcon);
        }
        if (player.gameObject.GetComponent<Character>().lives == 2)
        {
            GUI.Label(new Rect((Screen.width / 2) - 55, 0, 55, 55), livesIcon);
            GUI.Label(new Rect((Screen.width / 2), 0, 55, 55), livesIcon);
        }
        if (player.gameObject.GetComponent<Character>().lives == 1)
        {
            GUI.Label(new Rect((Screen.width / 2) - 55, 0, 55, 55), livesIcon);
        }
	}

	void Update()
	{
		Character playerHP = player.gameObject.GetComponent<Character>();
		Souls playerMP = player.gameObject.GetComponent<Souls>();
		life = (playerHP.CurHP / playerHP.MaxHP) * 388;
		power = (playerMP.Energy / playerMP.maxEnergy) * 388;

	}

}
