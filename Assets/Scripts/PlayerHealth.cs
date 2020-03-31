using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] float healthPoints = 20;

    private void OnTriggerEnter (Collider other) {
        healthPoints--;
    }
}