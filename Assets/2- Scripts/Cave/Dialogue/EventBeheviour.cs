using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventBeheviour : ScriptableObject
{
   public void TestEvent()
   {
      Debug.Log("test event successful");
   }
   
   public void TestEventCallForVideo()
   {
      Debug.Log("VideoPlay");
      RefGameObjectsEvent.instance.screenUnchanged.SetActive(true);
      RefGameObjectsEvent.instance.trolleyUnchanged.Play();
      RefGameObjectsEvent.instance.Switch();
   }

   public void TestEventCallForBattle()
   {
      //blackout whole the scene except fatman & player
      
      
      for (int i = 0; i < RefGameObjectsEvent.instance.battleCameraTriggers.Count; i++)
      {
         RefGameObjectsEvent.instance.battleCameraTriggers[i].SetActive(false);
      }
      RefGameObjectsEvent.instance.NPCs.SetActive(false);
      RefGameObjectsEvent.instance.fatManAnimator.SetBool("PinAttack", true);
      RefGameObjectsEvent.instance.battleBounds.SetActive(true);
      Destroy(RefGameObjectsEvent.instance.fatmanHealthCheck);
   }
}
