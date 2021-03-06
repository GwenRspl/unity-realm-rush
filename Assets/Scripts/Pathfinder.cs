﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    [SerializeField] GameObject pathTile;
    [SerializeField] Transform pathTileParent;

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
        if (path.Count == 0) {
            PreparePath ();
        }
        return path;
    }

    private void PreparePath () {
        LoadBlocks ();
        BreadthFirstSearch ();
        CreatePath ();
        AddTilesToPath ();
    }

    private void AddTilesToPath () {
        foreach (Waypoint waypoint in path) {
            var tile = Instantiate (pathTile, waypoint.transform.position, Quaternion.identity);
            tile.transform.parent = pathTileParent;
        }
    }

    private void CreatePath () {

        SetAsPath (endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint) {
            SetAsPath (previous);
            previous = previous.exploredFrom;
        }

        SetAsPath (startWaypoint);
        path.Reverse ();
    }

    private void SetAsPath (Waypoint waypoint) {
        path.Add (waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch () {
        queue.Enqueue (startWaypoint);

        while (queue.Count > 0 && isRunning) {
            currentSearchCenter = queue.Dequeue ();
            HaltIfEndFound ();
            ExploreNeighbours ();
            currentSearchCenter.isExplored = true;
        }

    }

    private void HaltIfEndFound () {
        if (currentSearchCenter == endWaypoint) {
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