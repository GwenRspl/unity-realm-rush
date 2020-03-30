using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    [SerializeField] GameObject deathFx;
    [SerializeField] Transform parent;
    [SerializeField] int hitPoints = 15;

    void Start () {
        //AddNonTriggerBoxCollider ();
    }

    private void AddNonTriggerBoxCollider () {
        Collider boxCollider = gameObject.AddComponent<BoxCollider> ();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision (GameObject other) {
        ProcessHit ();
        if (hitPoints <= 0) {
            KillEnemy ();
        }
    }

    private void ProcessHit () {
        hitPoints--;
    }

    private void KillEnemy () {
        GameObject fx = Instantiate (deathFx, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy (gameObject);
    }
}