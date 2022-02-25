using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    
    [Tooltip("Adds amount to maxHitPoints on death")]
    [SerializeField] int difficultyRamp = 1;

    int currentHitPoints = 0;

    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start() 
    {
        enemy = GetComponent<Enemy>();    
    }

    private void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;
        if(currentHitPoints <= 0)
        {
            enemy.RewardGold();
            maxHitPoints += difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
