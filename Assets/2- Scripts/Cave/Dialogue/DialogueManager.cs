using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    
    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }
    
    public GameObject dialogueBox;
    
    public Text dialogueName;
    public Text dialogueText;
    public Image dialoguePortrait;

    public float delay = 0.01f;

    public Queue<DialogueBase.Info> dialogueInfo;
    
    //option stuff
    private bool isDialogueOption;
    public GameObject dialogueOptionUI;
    public bool inDialogue;
    public GameObject[] optionButtons;
    private int optionsAmount;
    public Text questionText;
    public Text questionName;
    public Image questionportrait;
    

    //typing Stuff
    // private bool isCurrentlyTyping;
    // private string completeText;
    // private string currentText = "";
    
    public GameObject conversationButton;
    
    public bool InDialogue
    {
        get => inDialogue;
        private set => inDialogue = value;
    }
    
    
    private void Start()
    {
        dialogueInfo = new Queue<DialogueBase.Info>(); //FIFO Collection
    }
    
    
    public void EnqueueDialogue(DialogueBase db)
    {
        if (inDialogue) return;
        inDialogue = true;
       
        dialogueBox.SetActive(true);
        dialogueInfo.Clear();

        OptionsPerser(db);

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
            
        }
        
        DequeueDialogue();
    }
    
    
    public void DequeueDialogue()
    {
        //typing Stuff
        // if (isCurrentlyTyping == true)
        // {
        //     CompleteText();
        //     StopAllCoroutines();
        //     isCurrentlyTyping = false;
        //     return;
        // }
        
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        
        //typing Stuff
        //completeText = info.myText;
        
        //dialogue
        dialogueName.text = info.character.characterName;
        dialogueText.text = info.myText;
        info.ChangeEmotion();
        dialoguePortrait.sprite = info.character.CharacterPortrait;
        

        //typing Stuff
        //dialogueText.text = "";
        //StartCoroutine(TypeText(info));

    }

    /*private IEnumerator TypeText(DialogueBase.Info info)
    {

        //isCurrentlyTyping = true;
        //dialogueText.text = "";
        foreach (char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            //yield return null;
        }

        //isCurrentlyTyping = false;
    }*/
    
    //typing Stuff
    // private void CompleteText()
    // {
    //     dialogueText.text = completeText;
    // }

    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);

        OptionsLogic();
    }
    private void OptionsLogic()
    {
        if (isDialogueOption == true)
        {
            dialogueOptionUI.SetActive(true);
        }
        else
        {
            inDialogue = false;
        }
    }
    
    public void CloseOptions()
    {
        dialogueOptionUI.SetActive(false);
    }
    
    private void OptionsPerser(DialogueBase db)
    {
        if (db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;
            questionText.text = dialogueOptions.questionText;
            questionName.text = dialogueOptions.character.characterName;
            dialogueOptions.ChangeEmotion();
            questionportrait.sprite = dialogueOptions.character.CharacterPortrait;
            
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }
            
            for (int i = 0; i < optionsAmount; i++)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text =
                    dialogueOptions.optionsInfo[i].buttonName;
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                if (dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }
    }
    



    /*public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject dialogueBox;

    public Text dialogueName;
    public Text dialogueText;
    public Image dialoguePortrait;
    //public Animation dialogueAnim;
    public float delay = 0.1f;
    

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();
    
    //option stuff
    private bool isDialogueOption;
    public GameObject dialogueOptionUI;
    public bool inDialogue;
    public GameObject[] optionButtons;
    private int optionsAmount;
    public Text questionText;
    
    private bool isCurrentlyTyping;
    private string completeText;
    private string currentText = "";

    public Animator anim;
    private Sprite lastSprite;


    public void Start()
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
    }

    public void EnqueueDialogue(DialogueBase db)
    {
        if (inDialogue) return;
        inDialogue = true;
        
        dialogueBox.SetActive(true);
        dialogueInfo.Clear();

        OptionsPerser(db);
        
        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
            
        }

        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        /*if (isCurrentlyTyping == true)
        {
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }#1#
        
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.myText;
        
        dialogueName.text = info.character.characterName;
        dialogueText.text = info.myText;
        info.ChangeEmotion();
        
        lastSprite = info.character.CharacterPortrait;

        if (info.character.characterAOC != null)
        {
            anim.enabled = true;
            anim.runtimeAnimatorController = info.character.characterAOC;
            anim.SetBool("IsTalking", true);
            anim.Play(info.characterEmotion.ToString());
        }
        else
        {
            dialoguePortrait.sprite = info.character.CharacterPortrait;
        }
        

        ////////dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }
    
    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;
        AudioManager.instance.PlayClip(info.character.characterVoice);
        for (int i = 0; i < completeText.Length; i++)
        {
            currentText = completeText.Substring(0, i);
            FindObjectOfType<DialogueManager>().GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        
        /*foreach (char c in info.myText)
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
        }#1#
        isCurrentlyTyping = false;
        FinishTalking();
    }

    /*private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    private void FinishTalking()
    {
        anim.SetBool("IsTalking", false);
        anim.enabled = false;
        dialoguePortrait.sprite = lastSprite;
    }
    
    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
        OptionsLogic();
    }


    private void OptionsLogic()
    {
        if (isDialogueOption == true)
        {
            dialogueOptionUI.SetActive(true);
        }
        else
        {
            inDialogue = false;
        }
    }


    public void CloseOptions()
    {
        dialogueOptionUI.SetActive(false);
    }

    private void OptionsPerser(DialogueBase db)
    {
        if (db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;
            questionText.text = dialogueOptions.questionText;

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }

            for (int i = 0; i < optionsAmount; i++)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text =
                    dialogueOptions.optionsInfo[i].buttonName;
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                if (dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }
    }*/


}




/////////////////////////////////////////////////////////FIRST DIALOGUEMANAGER
//from "create dialogue system for your game" vid
/*public Image actorImage;
public Text actorName;
public Text messageText;
public RectTransform backgroundBox;

private Message[] currentMessages;
private Actor[] currentActors;
private int activeMessage = 0;
public static bool isActive = false;

//[SerializeField] private Animator fatManAnimator;

public void OpenDialogue(Message[] messages, Actor[] actors)
{
    currentMessages = messages;
    currentActors = actors;
    activeMessage = 0;
    isActive = true;
    DisplayMessage();
    backgroundBox.LeanScale(Vector3.one, 0.5f);
}

public void DisplayMessage()
{
    Message messageToDisplay = currentMessages[activeMessage];
    messageText.text = messageToDisplay.message;

    Actor actorToDisplay = currentActors[messageToDisplay.actorId];
    actorName.text = actorToDisplay.name;
    actorImage.sprite = actorToDisplay.sprite;
    AnimationTextColor();
}

public void NextMessage()
{
    activeMessage++;
    if (activeMessage < currentMessages.Length)
    {
        DisplayMessage();
    }
    else
    {
        Debug.Log("conversation ended");
        backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInExpo();
        isActive = false;
    }
}

void AnimationTextColor()
{
    LeanTween.textAlpha(messageText.rectTransform, 0, 0);
    LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
}


private void Start()
{
    backgroundBox.transform.localScale = Vector3.zero;
}

private void Update()
{
    if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
    {
        NextMessage();
    }
}*////////












/*public Text textDisplay;
    public string[] sentences;
    private int index;
    public float typeSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }*/