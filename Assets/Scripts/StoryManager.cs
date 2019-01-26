using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public List<Story> Stories;
    public object CurrentNode = null;
    public float CurrentProgress = 0.0f;
    public Story.NodeType CurrentNodeType = Story.NodeType.None;

    private int m_CurrentStoryIndex = 0;
    private int m_CurrentNodeIndex = 0;

    private int m_MainQuestNum = 0;
    private int m_CurrentMainQuestIndex = -1;

    void Start()
    {

    }

    void OnDestroy()
    {
        
    }

    public void StartStory()
    {
        m_CurrentNodeIndex = 0;
        m_CurrentStoryIndex = 0;
    }

    public bool ContinueStory()
    {
        if (m_CurrentStoryIndex >= Stories.Count)
        {
            return false;
        }

        Story CurrentStory = Stories[m_CurrentStoryIndex];
        Story.Node Node = CurrentStory.Nodes[m_CurrentNodeIndex];
        CurrentProgress = m_MainQuestNum == 0 ? 0.0f : (m_CurrentMainQuestIndex + 1.0f) / m_MainQuestNum;

        if (Node.StoryNode.Enable)
        {
            CurrentNodeType = Story.NodeType.Story;
            CurrentNode = Node.StoryNode;
            EventManager.Trigger(Event.STORY_TRIGGER, null);
        }
        else if (Node.BranchNode.Enable)
        {
            CurrentNodeType = Story.NodeType.Branch;
            CurrentNode = Node.BranchNode;
            EventManager.Trigger(Event.BRANCH_TRIGGER, null);
        }
        else if (Node.ActionNode.Enable)
        {
            CurrentNodeType = Story.NodeType.Action;
            CurrentNode = Node.ActionNode;
            EventManager.Trigger(Event.ACTION_TRIGGER, null);
        }

        if (m_CurrentNodeIndex < CurrentStory.Nodes.Count - 1)
        {
            m_CurrentNodeIndex++;
        }
        else
        {
            m_CurrentNodeIndex = 0;
            m_CurrentStoryIndex++;

            if (m_CurrentStoryIndex < Stories.Count && Stories[m_CurrentStoryIndex].IsMainQuest)
            {
                m_CurrentMainQuestIndex++;
            }
        }

        return true;
    }

    public void Init()
    {
        for (int i = 0; i < Stories.Count; i++)
        {
            if (Stories[i].IsMainQuest)
            {
                m_MainQuestNum++;
            }
        }
        if (Stories[0].IsMainQuest)
        {
            m_CurrentMainQuestIndex++;
        }
        Debug.Log("StoryManager Init.");
    }
}


