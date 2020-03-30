﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    void Start () {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder> ();
        var path = pathfinder.GetPath ();
        StartCoroutine (FollowPath (path)); // just like invoke() but allow for loop inside method

    }

    IEnumerator FollowPath (List<Waypoint> path) {
        foreach (Waypoint waypoint in path) {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds (1f);
        }
    }

}