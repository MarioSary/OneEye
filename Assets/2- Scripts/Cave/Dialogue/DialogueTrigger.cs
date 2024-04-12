using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueBase dialogueRef;
    public Enemy enemy;
    [SerializeField] public AudioSource fatManSigh;

    public void TriggerDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DialogueManager.instance.EnqueueDialogue(dialogueRef);
            DialogueManager.instance.conversationButton.SetActive(false);

            if (fatManSigh != null)
            {
                fatManSigh.Play();
            }
            
        }

    }
    private void Update()
    {
        if (enemy.isPlayerClose == true)
        {
            TriggerDialogue();
        }
    }
    
}





   



