using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<string> activeQuests = new List<string>(); 
    public List<string> completedQuests = new List<string>(); 

    public void AddQuest(string quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            Debug.Log("Quest Added: " + quest);
        }
    }

    public void CompleteQuest(string quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            Debug.Log("Quest Completed: " + quest);
        }
    }

    public bool IsQuestCompleted(string quest)
    {
        return completedQuests.Contains(quest);
    }
}