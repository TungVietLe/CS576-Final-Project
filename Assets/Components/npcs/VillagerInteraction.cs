using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VillagerInteraction : MonoBehaviour
{
    public string dialogue; 
    public TextMeshProUGUI interactionPrompt; 
    public DialogueManager dialogueManager; 

    private bool isPlayerInRange = false; 

    void Start()
    {
        interactionPrompt.gameObject.SetActive(false); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.gameObject.SetActive(true); 
            isPlayerInRange = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.gameObject.SetActive(false); 
            isPlayerInRange = false; 
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            dialogueManager.ShowDialogue(dialogue);
            interactionPrompt.gameObject.SetActive(false); 
        }
    }
}