using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectileParticle;
    // Update is called once per frame
    void Update () {
        if (targetEnemy) {
            objectToPan.LookAt (targetEnemy);
            FireAtEnemy ();
        } else {
            Shoot (false);
        }

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