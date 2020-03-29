using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Waypoint startWayPoint, endWayPoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint> ();
    Queue<Waypoint> queue = new Queue<Waypoint> ();
    bool isRunning = true;
    Waypoint currentSearchCenter;
    List<Waypoint> path = new List<Waypoint> ();

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath () {
        LoadBlocks ();
        ColorStartAndEnd ();
        BreadthFirstSearch ();
        CreatePath ();
        return path;
    }

    private void CreatePath () {
        path.Add (endWayPoint);
        Waypoint previous = endWayPoint.exploredFrom;
        while (previous != startWayPoint) {
            path.Add (previous);
            previous = previous.exploredFrom;
        }
        path.Add (startWayPoint);
        path.Reverse ();
    }

    private void BreadthFirstSearch () {
        queue.Enqueue (startWayPoint);

        while (queue.Count > 0 && isRunning) {
            currentSearchCenter = queue.Dequeue ();
            HaltIfEndFound ();
            ExploreNeighbours ();
            currentSearchCenter.isExplored = true;
        }

    }

    private void HaltIfEndFound () {
        if (currentSearchCenter == endWayPoint) {
            isRunning = false;
        }
    }

    private void ExploreNeighbours () {
        if (!isRunning) {
            return;
        }
        foreach (Vector2Int direction in directions) {
            Vector2Int neighbourCoordinates = currentSearchCenter.GetGridPos () + direction;
            if (grid.ContainsKey (neighbourCoordinates)) {
                QueueNewNeighbours (neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours (Vector2Int neighbourCoordinates) {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (!neighbour.isExplored && !queue.Contains (neighbour)) {
            queue.Enqueue (neighbour);
            neighbour.exploredFrom = currentSearchCenter;
        }
    }

    private void ColorStartAndEnd () {
        startWayPoint.SetTopColor (Color.green);
        endWayPoint.SetTopColor (Color.red);
    }

    private void LoadBlocks () {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint> ();
        foreach (Waypoint waypoint in waypoints) {

            var gridPos = waypoint.GetGridPos ();
            if (grid.ContainsKey (gridPos)) {
                Debug.LogWarning ("Skipping overlapping block " + waypoint);
            } else {
                grid.Add (gridPos, waypoint);
            }
        }
    }

}