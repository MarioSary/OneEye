using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrolleyQuestionUI : MonoBehaviour
{
   private TextMeshProUGUI textMeshPro;
   private Button choiceButton;
   private Button noChoiceButton;

   private void Awake()
   {
      textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
      choiceButton = transform.Find("ChoiceButton").GetComponent<Button>();
      noChoiceButton = transform.Find("NoChoiceButton").GetComponent<Button>();
      ShowQuestion("TrolleyTExt", () =>
      {
         Debug.Log("Yes");
      }, () =>
      {
         Debug.Log("No");
      });
   }

   public void ShowQuestion(string questionText, Action yesAction, Action noAction)
   {
      textMeshPro.text = questionText;
      choiceButton.onClick.AddListener(new UnityEngine.Events.UnityAction(yesAction));
      noChoiceButton.onClick.AddListener(new UnityEngine.Events.UnityAction(noAction));
   }
}
