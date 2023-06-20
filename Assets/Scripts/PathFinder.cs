using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    bool isRunning = true;
    Waypoint searchRoot;
    Queue<Waypoint> queue = new Queue<Waypoint>();
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public List<Waypoint> GetPath() {
        if (path.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint) {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }

        SetAsPath(startWaypoint);

        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint) {
        path.Add(waypoint);
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchRoot = queue.Dequeue();
            searchRoot.isExplored = true;
            StopIfEndFound();
            ExploreNeighbours();
        }
    }

    private void StopIfEndFound()
    {
        if (searchRoot == endWaypoint) {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) return;

        foreach (Vector2Int direction in directions) 
        {
            Vector2Int neighboursCoordinates = searchRoot.GetGridPos() + direction;
            if (grid.ContainsKey(neighboursCoordinates))
            {
                Waypoint neighbour = grid[neighboursCoordinates];
                if (neighbour.isExplored || queue.Contains(neighbour))
                {

                }
                else 
                {
                    queue.Enqueue(neighbour);
                    neighbour.exploredFrom = searchRoot;
                }
            }
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints) 
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping) 
            {
                Debug.LogWarning("Overlapping block " + waypoint);
            } 
            else 
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
