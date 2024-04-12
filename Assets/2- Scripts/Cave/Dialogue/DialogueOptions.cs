using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Dialogue Option", menuName = "DialogueOptions")]
public class DialogueOptions : DialogueBase
{
    [TextArea(1,10)]
    public string questionText;
    public CharacterProfile character;
    //public string questionName;
    //public Sprite questionportrait;
    
    public EmotionType characterEmotion;
        
    public void ChangeEmotion()
    {
        character.Emotion = characterEmotion;
    }
    
    
    [System.Serializable] public class Options
    {
        public string buttonName;
        public DialogueBase nextDialogue;
        public UnityEvent myEvent;
        
    }
    public Options[] optionsInfo;
}
