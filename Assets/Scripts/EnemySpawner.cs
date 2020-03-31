using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [Range (0.1f, 120f)][SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab; //we want to put only Enemy prefab, so this will only let us put a prefab that has an enemyMovement
    [SerializeField] Transform parent;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnEnemySFX;

    int enemiesSpawned = 0;

    void Start () {
        scoreText.text = enemiesSpawned.ToString ();
        StartCoroutine (RepeatedlySpawnEnemies ());
    }

    IEnumerator RepeatedlySpawnEnemies () {

        while (true) {
            GetComponent<AudioSource> ().PlayOneShot (spawnEnemySFX);

            EnemyMovement enemy = Instantiate (enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = parent;
            AddScore ();

            yield return new WaitForSeconds (secondsBetweenSpawns);
        }
    }

    private void AddScore () {
        enemiesSpawned++;
        scoreText.text = enemiesSpawned.ToString ();
    }
}