using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{

    public float Speed = 5f;
    public float Travelled = 0;
    public float LifeTime = 10;
    public int Strength;
    public GameObject Shooter;
    public Vector3 dir;

    void Start()
    {

    }

    void OnCollisionEnter (Collision hit)
    {
        bool desTROyit = hit.gameObject.name == "Enemy" || hit.gameObject.name == "Platform" || hit.gameObject.name == "Boundary";
        if (desTROyit)
            Destroy(gameObject);

    }

    void Update()
    {

        Vector3 translation = dir * Speed * Time.deltaTime;

        transform.Translate(translation);
        Travelled += translation.magnitude;


        if (Travelled >= LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
