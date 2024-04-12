using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem movementParticle;

    [Range(0, 10)] [SerializeField] private int occurAfterVelocity;
    [Range(0, 0.2f)] [SerializeField] private float dustFormationPeriod;

    [SerializeField] private Rigidbody2D playerRb;
    private float counter;
    void Update()
    {
        counter += Time.deltaTime;

        if (Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }
}
