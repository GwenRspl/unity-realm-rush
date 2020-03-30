using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tower : MonoBehaviour {

    //Parameters 
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectileParticle;

    public Waypoint baseWaypoint;

    //State
    Transform targetEnemy;

    void Update () {
        SetTargetEnemy ();
        if (targetEnemy) {
            objectToPan.LookAt (targetEnemy);
            FireAtEnemy ();
        } else {
            Shoot (false);
        }

    }

    private void SetTargetEnemy () {
        var sceneEnemies = FindObjectsOfType<EnemyDamage> ();
        if (sceneEnemies.Length != 0) {
            Transform closestEnemy = sceneEnemies[0].transform;

            foreach (EnemyDamage testEnemy in sceneEnemies) {
                closestEnemy = GetClosest (closestEnemy, testEnemy.transform);
            }

            targetEnemy = closestEnemy;
        }
    }

    private Transform GetClosest (Transform transformA, Transform transformB) {
        float distanceToA = Vector3.Distance (gameObject.transform.position, transformA.position);
        float distanceToB = Vector3.Distance (gameObject.transform.position, transformB.position);
        if (distanceToA < distanceToB) {
            return transformA;
        }
        return transformB;
    }

    private void FireAtEnemy () {
        float distanceToEnemy = Vector3.Distance (targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange) {
            Shoot (true);
        } else {
            Shoot (false);
        }
    }

    private void Shoot (bool isActive) {
        EmissionModule emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;

    }
}