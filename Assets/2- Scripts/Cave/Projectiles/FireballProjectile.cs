using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 direction;
    [SerializeField] private string targetTag;

    void FixedUpdate()
    {
        direction = new Vector3(0, transform.localScale.y);
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
