using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    [SerializeField] ParticleSystem deathFx;
    [SerializeField] int hitPoints = 15;
    [SerializeField] ParticleSystem hitParticlePrefab;

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
        hitParticlePrefab.Play ();
    }

    private void KillEnemy () {
        ParticleSystem fx = Instantiate (deathFx, transform.position, Quaternion.identity);
        Destroy (fx.gameObject, fx.main.duration);
        Destroy (gameObject);
    }
}