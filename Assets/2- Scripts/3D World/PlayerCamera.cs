using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float senseX;
    //public float senseY;

    public Transform orientation;
    
    private float xRotation;
    private float yRotation;
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
    }
    
    void FixedUpdate()
    {
        if (DialogueManager.instance.InDialogue)
        {
            return;
        }
        else
        {
            //get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
            //float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;
            
            yRotation += mouseX;
                    
            //xRotation -= mouseY;
                    
            //xRotation = Mathf.Clamp(xRotation, 0, 20f);
            //yRotation = Mathf.Clamp(yRotation, -90f, 90f);
                    
            //rotate camera and orientation
            transform.rotation = quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = quaternion.Euler(0, yRotation,0);
        }
        
    }
}
