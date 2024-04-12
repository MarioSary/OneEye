using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
   private bool collected = false;
   private float speed = 10;
   private Transform collectTransform;

   private void Start()
   {
      collectTransform = GameObject.Find("CollectTransform").transform;
   }

   private void Update()
   {
      Move();
   }

   private void Move()
   {
      if (collected)
      {
         transform.position =
            Vector3.MoveTowards(transform.position, collectTransform.position, Time.deltaTime * speed);

      }

      if (transform.position == collectTransform.position)
      {
         UIManager.Instance.AddGem();
         Destroy(gameObject);
      }
   }

   public void Collect()
   {
      collected = true;
      
   }
}
