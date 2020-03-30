using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    Queue<Tower> towerQueue = new Queue<Tower> ();

    public void AddTower (Waypoint waypoint) {
        if (towerQueue.Count < towerLimit) {
            InstanciateNewTower (waypoint);
        } else {
            MoveExistingTower (waypoint);
        }

    }

    private void MoveExistingTower (Waypoint waypoint) {
        Tower oldTower = towerQueue.Dequeue ();
        oldTower.baseWaypoint.isPlaceable = true;

        oldTower.baseWaypoint = waypoint;
        oldTower.transform.position = waypoint.transform.position;
        waypoint.isPlaceable = false;
        towerQueue.Enqueue (oldTower);

    }

    private void InstanciateNewTower (Waypoint waypoint) {
        Tower tower = Instantiate (towerPrefab, waypoint.transform.position, Quaternion.identity);
        waypoint.isPlaceable = false;
        tower.baseWaypoint = waypoint;
        towerQueue.Enqueue (tower);
    }
}