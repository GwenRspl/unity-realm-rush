using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFx;
    [SerializeField] Transform parent;
    [SerializeField] int hits = 15;

    void Start () {
        AddNonTriggerBoxCollider ();
    }

    private void AddNonTriggerBoxCollider () {
        Collider boxCollider = gameObject.AddComponent<BoxCollider> ();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision (GameObject other) {
        print ("something hit me! " + other + gameObject);
        ProcessHit ();
        if (hits <= 0) {
            KillEnemy ();
        }
    }

    private void ProcessHit () {
        hits--;
    }

    private void KillEnemy () {
        GameObject fx = Instantiate (deathFx, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy (gameObject);
    }
}