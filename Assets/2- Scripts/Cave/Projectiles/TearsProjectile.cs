using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearsProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 direction;
    [SerializeField] private string targetTag;

    void FixedUpdate()
    {
        Vector3 direction = Vector3.down;
        transform.Translate(direction * speed * Time.deltaTime);
        
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
