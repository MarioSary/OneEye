using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Profile", menuName = "Character Profile")]
public class CharacterProfile : ScriptableObject
{
   public string characterName;
   private Sprite characterPortrait;
   //public AudioClip characterVoice;

   public Sprite CharacterPortrait
   {
      get
      {
         SetEmotionType(Emotion);
         return characterPortrait;
      }
   }
   
   [System.Serializable]
   public class EmotionPortrait
   {
      public Sprite standard;
      public Sprite angry;
      public Sprite scared;

   }

   //public AnimatorOverrideController characterAOC;

   public EmotionPortrait emotionPortrait;

   public EmotionType Emotion { get; set; }

   public void SetEmotionType(EmotionType newEmotion)
   {
      Emotion = newEmotion;
      switch (Emotion)
      {
         case EmotionType.Standard:
            characterPortrait = emotionPortrait.standard;
            break;
         case EmotionType.Angry:
            characterPortrait = emotionPortrait.angry;
            break;
         case EmotionType.Scared:
            characterPortrait = emotionPortrait.scared;
            break;
         
      }
   }

}

public enum EmotionType
{
   Standard,
   Angry,
   Scared
}
