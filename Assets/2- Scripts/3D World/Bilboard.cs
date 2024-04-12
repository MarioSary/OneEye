using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
   public Camera theCam;
   public bool useStaticBilboard;
   private void LateUpdate()
   {
      if (!useStaticBilboard)
      {
         transform.LookAt(theCam.transform);
      }
      else
      {
         transform.rotation = theCam.transform.rotation;
      }

      transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
   }
}
