using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    [SerializeField] Tower towerPrefab;

    public bool isExplored = false; // ok as is a data class
    public Waypoint exploredFrom;
    public bool isPlaceable = true;

    Vector2Int gridPos;
    const int gridSize = 10;

    void Update () {

    }

    void OnMouseOver () {
        if (Input.GetMouseButtonDown (0)) {

            if (isPlaceable) {
                Instantiate (towerPrefab, transform.position, Quaternion.identity);
                isPlaceable = false;
            } else {
                print ("cant place here");
            }

        }
    }

    public int GetGridSize () {
        return gridSize;
    }

    public Vector2Int GetGridPos () {
        return new Vector2Int (
            Mathf.RoundToInt (transform.position.x / gridSize),
            Mathf.RoundToInt (transform.position.z / gridSize));
    }

}