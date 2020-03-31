using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    [SerializeField] ParticleSystem deathFx;
    [SerializeField] int hitPoints = 15;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource audioSource;

    void Start () {
        audioSource = GetComponent<AudioSource> ();
    }
    private void OnParticleCollision (GameObject other) {
        ProcessHit ();
        if (hitPoints <= 0) {
            KillEnemy ();
        }
    }

    private void ProcessHit () {
        audioSource.PlayOneShot (enemyHitSFX);
        hitPoints--;
        hitParticlePrefab.Play ();
    }

    private void KillEnemy () {
        ParticleSystem fx = Instantiate (deathFx, transform.position, Quaternion.identity);
        Destroy (fx.gameObject, fx.main.duration);

        AudioSource.PlayClipAtPoint (enemyDeathSFX, Camera.main.transform.position); //sound called this way will be 3D so if far from camera, we can't hear. So we have to put the sound next to the camera
        Destroy (gameObject);
    }
}