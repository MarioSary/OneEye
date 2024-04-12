using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Trolley : MonoBehaviour
{
    /*public float speed;
    public GameObject destination;
    [SerializeField] private GameObject target;*/

   
    [SerializeField] private float speed = 1.5f;
    private Rigidbody rb;
    public GameObject nextTarget;
    public float turningSpeed = 1f;
    public GameObject sensor;

    public static bool shouldExecute;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shouldExecute = true;
        Time.timeScale = 1;
    }

    public void FixedUpdate()
    {
        if (shouldExecute == true)
        {
            nextTarget = sensor.GetComponent<TargetSensor>().detectedtarget;
        
            if (nextTarget != null)
            {
                Vector3 lookPos = nextTarget.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
                   
                rb.velocity = transform.forward * speed; 
            }
        }
        

        /*if (nextTarget != null)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, nextTarget.transform.position, speed * Time.deltaTime);
            rb.MovePosition(pos);


            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextTarget.transform.position, step);

            /*if (Vector3.Distance(transform.position, nextTarget.transform.position) < 0.001f)
            {
                nextTarget.transform.position *= -1.0f;
            }

            /*transform.position = Vector3.MoveTowards(transform.position, nextTarget.transform.position, speed * Time.deltaTime);
            transform.forward = nextTarget.transform.position - transform.position;
            Quaternion rotation = quaternion.LookRotation(Vector3.forward,Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);*/
        }

    /*IEnumerator OnTriggerEnter(Collider col)
    {
        if (col.tag == "FreezeTarget")
        {
            //Debug.Log("stop point detected");

            shouldExecute = false;
            
            yield return new WaitForSeconds(58f);

            shouldExecute = true;
            speed = 8f;
        }
    }*/

}
