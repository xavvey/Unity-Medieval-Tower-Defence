using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{   
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0f, 50f)] int poolSize = 5;
    [SerializeField] [Range(1f, 30f)] float respawnDelay = 2f;

    GameObject[] pool;

    private void Awake() 
    {
        PopulatePool();    
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    private IEnumerator SpawnEnemy()
    {      
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void EnableObjectInPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
