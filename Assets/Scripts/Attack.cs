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


    void OnCollisionEnter (Collision hit)
    {
        bool playerAttack = hit.gameObject.tag == "Enemy" || hit.gameObject.name == "Platform" || hit.gameObject.name == "Boundary";
        if (playerAttack && Shooter.name == "Player")
        {
            Destroy(gameObject);
        }

        bool enemyAttack = hit.gameObject.tag == "Player" || hit.gameObject.name == "Platform" || hit.gameObject.name == "Boundary";
        if (enemyAttack && Shooter.name == "AngryEnemy")
        {
            Destroy(gameObject);
        }

        if (hit.gameObject.name == "Bullet(Clone)")
        { 
            Physics.IgnoreCollision(hit.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true); 
        }

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
