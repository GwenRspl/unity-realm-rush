using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] Animator anim;

    public void WalkForward (bool isActive) {
        anim.SetBool ("Walk Forward", isActive);
    }

    public void TakeDamage (bool isActive) {
        anim.SetBool ("Take Damage", isActive);
    }

    public void Die (bool isActive) {
        anim.SetBool ("Die", isActive);
    }
}