using UnityEngine;
using System.Collections;

public class Scores : MonoBehaviour {
   public float livesLost = 0;
   public float enemiesKilled = 0;
   public bool completedMap = false;
   public float powerUps = 0;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

}
