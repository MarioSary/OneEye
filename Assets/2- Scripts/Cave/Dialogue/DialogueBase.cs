using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public CharacterProfile character;
        [TextArea(4, 8)] public string myText;
        
        public EmotionType characterEmotion;
        
        public void ChangeEmotion()
        {
            character.Emotion = characterEmotion;
        }
        
        ////////Maryam:to have events after fatman final dialogue
        //public UnityEvent FinalDialogueEvent;
    }
    
    
    [Header("Insert Dialogue Information Below")]
    public Info[] dialogueInfo;
    

}
