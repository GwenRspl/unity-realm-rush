using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] float healthPoints = 20;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip playerDamageSFX;

    private void Start () {
        healthText.text = healthPoints.ToString ();
    }

    private void OnTriggerEnter (Collider other) {
        GetComponent<AudioSource> ().PlayOneShot (playerDamageSFX);
        healthPoints--;
        healthText.text = healthPoints.ToString ();
    }
}