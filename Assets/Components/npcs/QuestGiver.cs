using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public string questDescription;
    public bool questGiven = false;
    public bool questCompleted = false;
    public Animator animator;
    

    public bool isTalking = false;
    private QuestManager questManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        questManager = FindObjectOfType<QuestManager>(); 
        AnimatorControllerParameter[] parameters = animator.parameters;
        
    }

    public void Update()
    {
        if (isTalking)
        {
            animator.SetBool("IsTalking", true);
        }
        else
        {
            animator.SetBool("IsTalking", false);
        }
    }
    public void StartQuest()
    {
        if (!questGiven)
        {
            isTalking = true;
            animator.SetBool("IsTalking", true);

            Debug.Log("Quest Given: " + questDescription);
            questGiven = true;
            questManager.AddQuest(questDescription); 
        }
        else if (questGiven && !questCompleted)
        {
            Debug.Log("You still need to complete the quest: " + questDescription);
        }
        else if (questCompleted)
        {
            Debug.Log("Thank you for completing the quest!");
        }
    }

    public void CompleteQuest()
    {
        questCompleted = true;
        questManager.CompleteQuest(questDescription); 
    }

    public void StopTalking()
    {
        isTalking = false;
        animator.SetBool("IsTalking", false);
    }
}
