using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    void Start()
    {
        startNode = gridManager.Grid[startCoordinates];
        destinationNode = gridManager.Grid[destinationCoordinates];
        BreadthFirstSearch();
        BuildPath();
    }

    void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighbourCoordinates))
            {
                neighbours.Add(grid[neighbourCoordinates]);
            }
        }

        foreach(Node neigbours in neighbours)
        {
            if(!reached.ContainsKey(neigbours.coordinates) && neigbours.isWalkable)
            {
                neigbours.connectedTo = currentSearchNode;
                reached.Add(neigbours.coordinates, neigbours);
                frontier.Enqueue(neigbours);
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }
}
