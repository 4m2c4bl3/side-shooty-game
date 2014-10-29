﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float MaxHP = 5f;
    public float CurHP = 0f;
    public Souls EquippedSoul;
    public float lives = 3;
    public Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        CurHP = MaxHP;
    }


    void Damaged(int Power)
    {
        if (EquippedSoul.Defence < Power)
        { CurHP -= Power - EquippedSoul.Defence; }
    }
	public void pureDamaged(int Power)
	{
		CurHP -= Power;

	}

    void Update()
    {
        if (CurHP <= 0)
        { 
           // Destroy(gameObject); 
            
            if (gameObject.name == "Player")
            {
                lives -= 1;
                var scoreObj = GameObject.FindGameObjectWithTag("Score");
                scoreObj.GetComponent<Scores>().livesLost ++;
                CurHP = MaxHP;
                transform.position = startPos;
            }
            if (gameObject.tag == "Enemy")
            {
                Destroy(gameObject); 
                var scoreObj = GameObject.FindGameObjectWithTag("Score");
                scoreObj.GetComponent<Scores>().enemiesKilled++;
            }
        }

        if (lives <= 0)
        {
            Application.LoadLevel("over");
        }
    }
    void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.name == "LevelComplete" && gameObject.name == "Player")
        {
            var scoreOtron = GameObject.FindGameObjectWithTag("Score");
            scoreOtron.GetComponent<Scores>().completedMap = true;
            Application.LoadLevel("over");
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (gameObject.tag == "Enemy")
        {
            Attack enemyattack = collision.gameObject.GetComponent<Attack>();

            if (enemyattack != null)
            {
                Damaged(enemyattack.Strength);
            }
        }
    }

    void GotHit (Collision collision)
    {
        Attack enemyattack = collision.gameObject.GetComponent<Attack>();
        
        if (enemyattack != null)
        {
            Damaged(enemyattack.Strength);
        }
        if (collision.gameObject.tag == "Enemy" && gameObject.name == "Player")
        {
            Damaged(collision.gameObject.GetComponent<Souls>().Strength);
        }

    }

}
