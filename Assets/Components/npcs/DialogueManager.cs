using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; 
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HideDialogue(); 
    }

    public void ShowDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
        canvasGroup.alpha = 1; 
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideDialogue()
    {
        canvasGroup.alpha = 0; 
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
