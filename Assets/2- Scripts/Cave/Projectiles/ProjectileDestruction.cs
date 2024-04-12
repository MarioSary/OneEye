using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestruction : MonoBehaviour
{
    [SerializeField] private Animator projectileAnim;
    [SerializeField] private AudioSource projectileSound;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Debug.Log("CollisionDetected");
            projectileAnim.SetTrigger("GroundEnter");
            projectileSound.Play();

        }
    }

    public void RockDestruction()
    {
        Destroy(gameObject);
    }


}
