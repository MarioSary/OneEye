using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CaveRockProjectile : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private GameObject target;
    private Rigidbody2D rb;
    public float force;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = target.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        



        float rockRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = quaternion.Euler(0, 0, rockRotation);

    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == targetTag)
        {
            col.GetComponentInParent<IHitable>().TakeHit();
            Destroy(gameObject);
        }
    }
    
}
