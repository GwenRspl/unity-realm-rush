using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Range (0.1f, 120f)][SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab; //we want to put only Enemy prefab, so this will only let us put a prefab that has an enemyMovement
    [SerializeField] Transform parent;

    void Start () {
        StartCoroutine (RepeatedlySpawnEnemies ());
    }

    IEnumerator RepeatedlySpawnEnemies () {

        while (true) {
            EnemyMovement enemy = Instantiate (enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = parent;
            yield return new WaitForSeconds (secondsBetweenSpawns);
        }
    }
}