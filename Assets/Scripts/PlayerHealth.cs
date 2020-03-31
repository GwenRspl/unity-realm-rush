using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] float healthPoints = 20;
    [SerializeField] Text healthText;

    private void Start() {
        healthText.text = healthPoints.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        healthPoints--;
        healthText.text = healthPoints.ToString();
    }
}