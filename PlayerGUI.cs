using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	public Texture2D powerBoarder;
	public Texture2D powerMeasure;
	public Texture2D healthBorder;
	public Texture2D healthMeasure;
	public float power = 1.0f;
	public float life = 1.0f;
	public GameObject player;
	void Start ()
	{


		}
	void OnGUI(){
		GUI.BeginGroup (new Rect (1,0,64,(power)));
		GUI.Label (new Rect (0,0,64,400), powerBoarder);
		GUI.Label (new Rect (0,0,64,400), powerMeasure);
		GUI.EndGroup ();

		GUI.BeginGroup (new Rect ((Screen.width-50),0,64,(life)));
		GUI.Label (new Rect (0,0,64,400), healthBorder);
		GUI.Label (new Rect (0,0,64,400), healthMeasure);
		GUI.EndGroup ();
	}

	void Update()
	{
		Character playerHP = player.gameObject.GetComponent<Character>();
		Souls playerMP = player.gameObject.GetComponent<Souls>();
		life = (playerHP.CurHP / playerHP.MaxHP) * 400;
		power = (playerMP.Energy / playerMP.maxEnergy) * 400;

	}

}
