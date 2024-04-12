using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //[SerializeField] private string targetTag;
    private void OnTriggerEnter2D(Collider2D col)
    {
        IHitable hit = col.GetComponentInParent<IHitable>();

        if (hit != null)
        {
            hit.TakeHit();
        }
    }
    
    
}
