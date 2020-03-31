using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float movementPeriod = .25f;
    [SerializeField] ParticleSystem goalParticle;

    void Start () {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder> ();
        var path = pathfinder.GetPath ();
        StartCoroutine (FollowPath (path)); // just like invoke() but allow for loop inside method

    }

    IEnumerator FollowPath (List<Waypoint> path) {
        foreach (Waypoint waypoint in path) {
            this.transform.LookAt (waypoint.transform);
            FindObjectOfType<EnemyController> ().WalkForward (true);
            this.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds (movementPeriod);
        }
        SelfDestruct ();
    }

    private void SelfDestruct () {
        Vector3 particlePosition = new Vector3 (transform.position.x, 10f, transform.position.z);
        ParticleSystem fx = Instantiate (goalParticle, particlePosition, Quaternion.identity);
        Destroy (fx.gameObject, fx.main.duration);
        Destroy (gameObject);
    }

}