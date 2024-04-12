using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class NPCs : MonoBehaviour, ICollisionHandler
{
    public DialogueBase dialogueRef;
    [SerializeField] private AudioSource[] NPCsSound;
    
    
    [HideInInspector] public bool isPlayerClose = false;
    [SerializeField] private Animator footAnim;

    public void Update()
    {
        if (isPlayerClose)
        {
            TriggerDialogue();
        }
    }
    
    public void TriggerDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (footAnim != null)
            {
                footAnim.SetTrigger("Dialogue");
            }
            
            if (DialogueManager.instance.conversationButton != null)
            {
                DialogueManager.instance.conversationButton.SetActive(false);
            }
            
            DialogueManager.instance.EnqueueDialogue(dialogueRef);
            DialogueManager.instance.conversationButton.SetActive(false);
            if (NPCsSound != null)
            {
                NPCsSound[Index.FromStart(0)].Play();
            }
            
        }

    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "NPC" && other.tag == "Player")
        {
            Debug.Log("NPC detected");
            if (DialogueManager.instance.conversationButton != null)
            {
                DialogueManager.instance.conversationButton.SetActive(true);
            }
            //NPCsSound[Index.FromStart(0)].Play();
            isPlayerClose = true;
        }
    }

    public void CollisionExit(string colliderName, GameObject other)
    {
        if (colliderName == "NPC" && other.tag == "Player")
        {
            Debug.Log("dialogue Exit");
            NPCsSound[Index.FromEnd(1)].Stop();
            isPlayerClose = false;
            if (DialogueManager.instance.conversationButton != null)
            {
                DialogueManager.instance.conversationButton.SetActive(false);
            }
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NPC detected");
            if (DialogueManager.instance.conversationButton != null)
            {
                DialogueManager.instance.conversationButton.SetActive(true);
            }
            isPlayerClose = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("dialogue Exit");
            isPlayerClose = false;
            if (DialogueManager.instance.conversationButton != null)
            {
                DialogueManager.instance.conversationButton.SetActive(false);
            }
        }
    }
}
