using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Slider m_StationSlider;

    private Text m_PropertyHappyText;
    private Image m_PropertyHappyIcon;
    private Text m_PropertyHungryText;
    private Image m_PropertyHungryIcon;
    private Text m_PropertyMoneyText;
    private Image m_PropertyMoneyIcon;

    private GameObject m_ProgressGO;
    private Text m_ProgressText;
    private Slider m_ProgressSlider;

    private GameObject m_SelectGO;
    private Button m_SelectLeftButton;
    private Button m_SelectRightButton;
    private Text m_SelectLeftText;
    private Text m_SelectRightText;

    private GameObject m_StoryGO;
    private Text m_StoryText;

    private GameObject m_AvatarGO;
    private Image m_AvatarIcon;
    private Text m_AvatarName;

    private GameObject m_ActionGO;
    private Button m_ActionLeftButton;
    private Button m_ActionRightButton;
    private Text m_ActionLeftText;
    private Text m_ActionRightText;

    void Start()
    {
        
    }

    public void Init()
    {
        m_StationSlider = GameObject.Find("Station").GetComponentInChildren<Slider>();
        m_StationSlider.interactable = false;

        m_PropertyHappyText = GameObject.Find("Happy").GetComponentInChildren<Text>();
        m_PropertyHappyIcon = GameObject.Find("Happy").GetComponentInChildren<Image>();
        m_PropertyHappyIcon.sprite = null;
        m_PropertyHungryText = GameObject.Find("Hungry").GetComponentInChildren<Text>();
        m_PropertyHungryIcon = GameObject.Find("Hungry").GetComponentInChildren<Image>();
        m_PropertyHungryIcon.sprite = null;
        m_PropertyMoneyText = GameObject.Find("Money").GetComponentInChildren<Text>();
        m_PropertyMoneyIcon = GameObject.Find("Money").GetComponentInChildren<Image>();
        m_PropertyMoneyIcon.sprite = null;

        m_ProgressGO = GameObject.Find("Progress");
        m_ProgressText = GameObject.Find("Progress").GetComponentInChildren<Text>();
        m_ProgressSlider = GameObject.Find("Progress").GetComponentInChildren<Slider>();
        m_ProgressSlider.interactable = false;

        m_SelectGO = GameObject.Find("Select");
        m_SelectLeftButton = GameObject.Find("SelectLeft").GetComponentInChildren<Button>();
        m_SelectRightButton = GameObject.Find("SelectRight").GetComponentInChildren<Button>();
        m_SelectLeftText = GameObject.Find("SelectLeft").GetComponentInChildren<Text>();
        m_SelectRightText = GameObject.Find("SelectRight").GetComponentInChildren<Text>();

        m_AvatarGO = GameObject.Find("Avatar");
        m_AvatarIcon = GameObject.Find("AvatarIcon").GetComponentInChildren<Image>();
        m_AvatarName = GameObject.Find("AvatarName").GetComponentInChildren<Text>();

        m_StoryGO = GameObject.Find("Story");
        m_StoryText = GameObject.Find("StoryText").GetComponentInChildren<Text>();

        m_ActionGO = GameObject.Find("Action");
        m_ActionLeftButton = GameObject.Find("ActionLeft").GetComponentInChildren<Button>();
        m_ActionRightButton = GameObject.Find("ActionRight").GetComponentInChildren<Button>();
        m_ActionLeftText = GameObject.Find("ActionLeft").GetComponentInChildren<Text>();
        m_ActionRightText = GameObject.Find("ActionRight").GetComponentInChildren<Text>();

        BindListener();

        Debug.Log("UIManager Init.");
    }

    public void SetActionVisible(bool IsVisible)
    {
        if (m_ActionGO != null)
        {
            m_ActionGO.SetActive(IsVisible);
        }
    }

    public void SetSelectVisible(bool IsVisible)
    {
        if (m_SelectGO != null)
        {
            m_SelectGO.SetActive(IsVisible);
        }
    }

    public void SetStoryTextVisible(bool IsVisible)
    {
        if (m_StoryGO != null)
        {
            m_StoryGO.SetActive(IsVisible);
        }
    }

    public void SetAvatorVisible(bool IsVisible)
    {
        if (m_AvatarGO != null)
        {
            m_AvatarGO.SetActive(IsVisible);
        }
    }

    public void SetProgressVisible(bool IsVisible)
    {
        if (m_ProgressGO != null)
        {
            m_ProgressGO.SetActive(IsVisible);
        }
    }

    public void SetStoryText(string Text)
    {
        if (m_StoryText != null)
        {
            m_StoryText.text = Text;
        }
    }

    public void SetSelectText(string TextLeft, string TextRight)
    {
        if (m_SelectLeftText != null)
        {
            m_SelectLeftText.text = TextLeft;
        }

        if (m_SelectRightText != null)
        {
            m_SelectRightText.text = TextRight;
        }
    }

    public void SetHappyValue(int Value)
    {
        if (m_PropertyHappyText != null)
        {
            m_PropertyHappyText.text = string.Format("心情 : {0}", Value);
        }
    }

    public void SetHungryValue(int Value)
    {
        if (m_PropertyHungryText != null)
        {
            m_PropertyHungryText.text = string.Format("饥饿度 : {0}", Value);
        }
    }

    public void SetMoneyValue(int Value)
    {
        if (m_PropertyMoneyText != null)
        {
            m_PropertyMoneyText.text = string.Format("金钱 : {0}", Value);
        }
    }

    public void ProgressCountDown(string Text, float Seconds, Action Callback)
    {
        if (m_ProgressText != null)
        {
            m_ProgressText.text = Text;
        }
        if (m_ProgressSlider != null)
        {
            StartCoroutine(ProgressCountDownCoroutine(Seconds, Callback));
        }
    }

    public void SetStationProgress(float Progress)
    {
        if (m_StationSlider != null)
        {
            m_StationSlider.value = Progress;
        }
    }

    public void SetAvatorData(Sprite Icon, string Name)
    {
        if (m_AvatarIcon != null)
        {
            m_AvatarIcon.sprite = Icon;
        }
        if (m_AvatarName != null)
        {
            m_AvatarName.text = Name;
        }
    }

    public void SetActionText(string LeftActionText, string RightActionText)
    {
        if (m_ActionLeftText != null)
        {
            m_ActionLeftText.text = LeftActionText;
        }
        if (m_ActionRightText != null)
        {
            m_ActionRightText.text = RightActionText;
        }
    }

    private IEnumerator ProgressCountDownCoroutine(float Seconds, Action Callback)
    {
        float TotalTime = Seconds;
        const float TIME_STEP = 0.1f;
        while (Seconds > 0.0f)
        {
            Seconds -= TIME_STEP;
            float Percentage = Seconds / TotalTime;
            m_ProgressSlider.value = Percentage;
            yield return new WaitForSeconds(TIME_STEP);
        }
        Callback();
    }

    private void BindListener()
    {
        UIEventListener.Get(m_StoryGO).onPointerClick += (go, data) =>
        {
            EventManager.Trigger(Event.STORY_TEXT_NEXT, null);
        };

        m_SelectLeftButton.onClick.AddListener(() =>
        {
            EventManager.Trigger(Event.BRANCH_SELECT_LEFT, null);
        });

        m_SelectRightButton.onClick.AddListener(() =>
        {
            EventManager.Trigger(Event.BRANCH_SELECT_RIGHT, null);
        });

        m_ActionLeftButton.onClick.AddListener(() =>
        {
            EventManager.Trigger(Event.ACTION_CHOOSE_LEFT, null);
        });

        m_ActionRightButton.onClick.AddListener(() =>
        {
            EventManager.Trigger(Event.ACTION_CHOOSE_RIGHT, null);
        });
    }
}
