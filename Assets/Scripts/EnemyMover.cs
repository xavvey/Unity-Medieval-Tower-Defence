using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> enemyPath = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float moveSpeed = 1f;

    Enemy enemy;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowEnemyPath());
    }

    private void Start() 
    {
        enemy = GetComponent<Enemy>();    
    }

    private void FindPath()
    {
        enemyPath.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            
            if(waypoint != null)
            {
                enemyPath.Add(waypoint);
            }
        }
    }

    private void ReturnToStart()
    {
        transform.position = enemyPath[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    private IEnumerator FollowEnemyPath()
    {
        foreach(Waypoint waypoint in enemyPath)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1)
            {
                travelPercent += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); 
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
