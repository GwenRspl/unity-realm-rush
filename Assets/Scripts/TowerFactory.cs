using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform parent;

    Queue<Tower> towerQueue = new Queue<Tower> ();

    public void AddTower (Waypoint waypoint) {
        if (towerQueue.Count < towerLimit) {
            InstanciateNewTower (waypoint);
        } else {
            MoveExistingTower (waypoint);
        }

    }

    private void InstanciateNewTower (Waypoint waypoint) {
        Tower tower = Instantiate (towerPrefab, waypoint.transform.position, Quaternion.identity);
        tower.transform.parent = parent;
        waypoint.isPlaceable = false;
        tower.baseWaypoint = waypoint;
        towerQueue.Enqueue (tower);
    }

    private void MoveExistingTower (Waypoint newBaseWaypoint) {
        Tower oldTower = towerQueue.Dequeue ();
        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;
        towerQueue.Enqueue (oldTower);
    }
}