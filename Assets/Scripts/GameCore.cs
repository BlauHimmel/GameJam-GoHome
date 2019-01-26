using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    private UIManager m_UIManager;
    private StoryManager m_StoryManager;
    private AudioManager m_AudioManager;

    private int m_Happy = 100;
    private int m_Hungry = 0;
    private int m_Money = 10000;
    private HashSet<string> m_SelectItem = new HashSet<string>();

	void Start ()
    {
        EventManager.Register(Event.GAME_TRIGGER, EventCallback);
        EventManager.Register(Event.GAME_SUBMIT, EventCallback);
        EventManager.Register(Event.SELECT_ITEM, EventCallback);
        EventManager.Register(Event.DESELECT_ITEM, EventCallback);

        EventManager.Register(Event.STORY_TRIGGER, EventCallback);
        EventManager.Register(Event.STORY_TEXT_NEXT, EventCallback);

        EventManager.Register(Event.ACTION_TRIGGER, EventCallback);
        EventManager.Register(Event.ACTION_CHOOSE_LEFT, EventCallback);
        EventManager.Register(Event.ACTION_CHOOSE_RIGHT, EventCallback);

        EventManager.Register(Event.BRANCH_TRIGGER, EventCallback);
        EventManager.Register(Event.BRANCH_SELECT_LEFT, EventCallback);
        EventManager.Register(Event.BRANCH_SELECT_RIGHT, EventCallback);

        m_UIManager = GetComponent<UIManager>();
        m_StoryManager = GetComponent<StoryManager>();
        m_AudioManager = GetComponent<AudioManager>();

        m_UIManager.Init();
        m_UIManager.LoadTrainItems(m_StoryManager.Items);
        m_StoryManager.Init();
        m_AudioManager.Init();

        m_UIManager.SetActionVisible(false);
        m_UIManager.SetSelectVisible(false);
        m_UIManager.SetStoryTextVisible(false);
        m_UIManager.SetAvatorVisible(false);
        m_UIManager.SetProgressVisible(false);
        m_UIManager.SetGamePanelVisible(false);

        m_UIManager.SetHappyValue(m_Happy);
        m_UIManager.SetHungryValue(m_Hungry);
        m_UIManager.SetMoneyValue(m_Money);

        m_StoryManager.StartStory();
        m_StoryManager.ContinueStory();
    }

    void OnDestroy()
    {
        EventManager.Remove(Event.GAME_TRIGGER, EventCallback);
        EventManager.Remove(Event.GAME_SUBMIT, EventCallback);
        EventManager.Remove(Event.SELECT_ITEM, EventCallback);
        EventManager.Remove(Event.DESELECT_ITEM, EventCallback);

        EventManager.Remove(Event.STORY_TRIGGER, EventCallback);
        EventManager.Remove(Event.STORY_TEXT_NEXT, EventCallback);

        EventManager.Remove(Event.ACTION_TRIGGER, EventCallback);
        EventManager.Remove(Event.ACTION_CHOOSE_LEFT, EventCallback);
        EventManager.Remove(Event.ACTION_CHOOSE_RIGHT, EventCallback);

        EventManager.Remove(Event.BRANCH_TRIGGER, EventCallback);
        EventManager.Remove(Event.BRANCH_SELECT_LEFT, EventCallback);
        EventManager.Remove(Event.BRANCH_SELECT_RIGHT, EventCallback);
    }

    private void EventCallback(object[] Args)
    {
        /////////////////////////////////////////////////////////////////////////////////////
        if (EventManager.CurrentEvent == Event.ACTION_TRIGGER)
        {
            Story.ActionNode Action = (Story.ActionNode)m_StoryManager.CurrentNode;
            m_UIManager.SetActionVisible(true);
            m_UIManager.SetActionText(Action.LeftActionText, Action.RightActionText);
        }
        else if (EventManager.CurrentEvent == Event.ACTION_CHOOSE_LEFT)
        {
            Story.ActionNode Action = (Story.ActionNode)m_StoryManager.CurrentNode;
            m_UIManager.SetActionVisible(false);
            m_UIManager.SetProgressVisible(true);
            m_UIManager.ProgressCountDown(Action.LeftActionText, Action.LeftActionTime, () =>
            {
                Story.PropertyNode PropertyNode = Action.LeftActionPropertyCast;
                UpdatePropertyByNode(PropertyNode);
                m_UIManager.SetProgressVisible(false);
                m_StoryManager.ContinueStory();
                m_UIManager.SetStationProgress(m_StoryManager.CurrentProgress);
            });
        }
        else if (EventManager.CurrentEvent == Event.ACTION_CHOOSE_RIGHT)
        {
            Story.ActionNode Action = (Story.ActionNode)m_StoryManager.CurrentNode;
            m_UIManager.SetActionVisible(false);
            m_UIManager.SetProgressVisible(true);
            m_UIManager.ProgressCountDown(Action.RightActionText, Action.RightActionTime, () =>
            {
                Story.PropertyNode PropertyNode = Action.RightActionPropertyCast;
                UpdatePropertyByNode(PropertyNode);
                m_UIManager.SetProgressVisible(false);
                m_StoryManager.ContinueStory();
                m_UIManager.SetStationProgress(m_StoryManager.CurrentProgress);
            });
        }
        /////////////////////////////////////////////////////////////////////////////////////
        else if (EventManager.CurrentEvent == Event.STORY_TRIGGER)
        {
            Story.StoryNode Story = (Story.StoryNode)m_StoryManager.CurrentNode;
            m_UIManager.SetStoryTextVisible(true);
            m_UIManager.SetStoryText(Story.Text);
            m_UIManager.SetAvatorVisible(true);
            m_UIManager.SetAvatorData(null, Story.CharacterName);

            if (Story.AudioPath.Length > 0)
            {
                m_AudioManager.SoundAllPause();
                m_AudioManager.SoundPlay(Story.AudioPath);
            }
        }
        else if (EventManager.CurrentEvent == Event.STORY_TEXT_NEXT)
        {
            m_StoryManager.ContinueStory();
        }
        /////////////////////////////////////////////////////////////////////////////////////
        else if (EventManager.CurrentEvent == Event.BRANCH_TRIGGER)
        {
            Story.BranchNode Branch = (Story.BranchNode)m_StoryManager.CurrentNode;
            m_UIManager.SetSelectVisible(true);
            m_UIManager.SetSelectText(Branch.LeftBranchText, Branch.RightBranchText);
        }
        else if (EventManager.CurrentEvent == Event.BRANCH_SELECT_LEFT)
        {
            Story.BranchNode Branch = (Story.BranchNode)m_StoryManager.CurrentNode;
            UpdatePropertyByNode(Branch.LeftBranchPropertyCast);
            m_UIManager.SetSelectVisible(false);
            m_UIManager.SetStoryTextVisible(false);
            m_UIManager.SetAvatorVisible(false);
            m_UIManager.SetStationProgress(m_StoryManager.CurrentProgress);
            m_StoryManager.ContinueStory();
        }
        else if (EventManager.CurrentEvent == Event.BRANCH_SELECT_RIGHT)
        {
            Story.BranchNode Branch = (Story.BranchNode)m_StoryManager.CurrentNode;
            UpdatePropertyByNode(Branch.RightBranchPropertyCast);
            m_UIManager.SetSelectVisible(false);
            m_UIManager.SetStoryTextVisible(false);
            m_UIManager.SetAvatorVisible(false);
            m_UIManager.SetStationProgress(m_StoryManager.CurrentProgress);
            m_StoryManager.ContinueStory();
        }
        /////////////////////////////////////////////////////////////////////////////////////
        else if (EventManager.CurrentEvent == Event.GAME_TRIGGER)
        {
            Story.GameNode Game = (Story.GameNode)m_StoryManager.CurrentNode;
            m_UIManager.SetGamePanelText(Game.Text);
            m_UIManager.SetGamePanelVisible(true);

            if (Game.AudioPath.Length > 0)
            {
                m_AudioManager.SoundAllPause();
                m_AudioManager.SoundPlay(Game.AudioPath);
            }
        }
        else if (EventManager.CurrentEvent == Event.GAME_SUBMIT)
        {
            Story.GameNode Game = (Story.GameNode)m_StoryManager.CurrentNode;
            bool Success = m_SelectItem.IsSubsetOf(Game.RequestItems);
            m_SelectItem.Clear();

            if (Success)
            {
                UpdatePropertyByNode(Game.SuccessPropertyCast);
                m_UIManager.SetGamePanelVisible(false);
                m_StoryManager.ContinueStory();
            }
            else
            {
                UpdatePropertyByNode(Game.FailurePropertyCast);
                m_UIManager.ClearItemsButtonMark();
            }
        }
        else if (EventManager.CurrentEvent == Event.SELECT_ITEM)
        {
            string ItemName = (string)Args[0];
            m_SelectItem.Add(ItemName);
        }
        else if (EventManager.CurrentEvent == Event.DESELECT_ITEM)
        {
            string ItemName = (string)Args[0];
            m_SelectItem.Remove(ItemName);
        }
        /////////////////////////////////////////////////////////////////////////////////////
    }

    private void UpdatePropertyByNode(Story.PropertyNode PropertyNode)
    {
        if (PropertyNode.CastAttribute == Story.Attribute.Happy)
        {
            m_Happy += PropertyNode.AttributeNumber;
            m_UIManager.SetHappyValue(m_Happy);
        }
        else if (PropertyNode.CastAttribute == Story.Attribute.Hungry)
        {
            m_Hungry += PropertyNode.AttributeNumber;
            m_UIManager.SetHungryValue(m_Hungry);
        }
        else if (PropertyNode.CastAttribute == Story.Attribute.Money)
        {
            m_Money += PropertyNode.AttributeNumber;
            m_UIManager.SetMoneyValue(m_Money);
        }
    }
}
