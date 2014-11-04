using UnityEngine;
using System.Collections;

public class newcamFloor : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player")
        {
            newcamChar.main.grounded = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "player")
        {
            if (newcamChar.main.grounded)
            {
                newcamChar.main.grounded = false;
            }
        }
    }
}
