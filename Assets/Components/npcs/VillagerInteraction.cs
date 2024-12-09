using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VillagerInteraction : MonoBehaviour
{
    public GameObject d_template;
    public GameObject canva;

    private bool isPlayerInRange = false; 

    void Start()
    {
         
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; 
            Debug.Log("PLayer in range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; 
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !PlayerMovement.dialogue) 
        {
            canva.SetActive(true);
            PlayerMovement.dialogue = true;
            Debug.Log("Conversation started");
            NewDialogue("Hello there!"); 
            canva.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void NewDialogue(string text)
    {
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }
}