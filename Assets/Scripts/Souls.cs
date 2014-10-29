using UnityEngine;
using System.Collections;

public class Souls : MonoBehaviour {

    public int Strength = 1;
    public int Defence = 1;
    public int Speed = 1;
	public float Energy = 1f;
	public float maxEnergy = 1f;
	public float useEnergy = 0.1f;
	public float resEnergy = 0.1f;
	public float respeedEnergy = 1.5f;
	public string EquippedSoul = "Hollow";
	float lastUpdate;
   // public float AttSpeed = 3f;

    void BaseSoul()
    {
		EquippedSoul = "Fractured Soul";
        Strength = 2;
        Defence = 1;
        Speed = 20;
		maxEnergy = 1;
		respeedEnergy = 1.5f;
       // AttSpeed = 0.5f;

    }

	void Start ()
	{
		lastUpdate = Time.time;

		}

	void Recover()
	{
		
		if(Time.time - lastUpdate >= respeedEnergy)
		{
			if (Energy < maxEnergy)
			{
				
				Energy += resEnergy;
				
				lastUpdate = Time.time;
				
				
				}
			}
		}

	void Update()
	{
		Recover ();
	}
}
