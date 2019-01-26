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

    private GameObject m_GamePanelGO;
    private Text m_GameText;
    private Button[] m_TrainIdxTabButtons;
    private GameObject[] m_ItemButtonsPanelGo;
    private Dictionary<string, Text> m_StringTextDict = new Dictionary<string, Text>();
    private Button m_OKButton;

    private Image m_BackgroundImage;

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

        m_GamePanelGO = GameObject.Find("GamePanel");
        m_GameText = GameObject.Find("GameText").GetComponentInChildren<Text>();

        m_TrainIdxTabButtons = new Button[4];
        m_TrainIdxTabButtons[0] = GameObject.Find("TabBtn0").GetComponentInChildren<Button>();
        m_TrainIdxTabButtons[1] = GameObject.Find("TabBtn1").GetComponentInChildren<Button>();
        m_TrainIdxTabButtons[2] = GameObject.Find("TabBtn2").GetComponentInChildren<Button>();
        m_TrainIdxTabButtons[3] = GameObject.Find("TabBtn3").GetComponentInChildren<Button>();

        m_ItemButtonsPanelGo = new GameObject[4];
        m_ItemButtonsPanelGo[0] = GameObject.Find("ItemButtons0");
        m_ItemButtonsPanelGo[1] = GameObject.Find("ItemButtons1");
        m_ItemButtonsPanelGo[2] = GameObject.Find("ItemButtons2");
        m_ItemButtonsPanelGo[3] = GameObject.Find("ItemButtons3");

        m_OKButton = GameObject.Find("OKButton").GetComponentInChildren<Button>();

        m_BackgroundImage = GameObject.Find("BackgroundImage").GetComponentInChildren<Image>();

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

    public void SetGamePanelVisible(bool IsVisible)
    {
        if (m_GamePanelGO != null)
        {
            m_GamePanelGO.SetActive(IsVisible);
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

    public void LoadTrainItems(TrainItems Items)
    {
        GameObject[] ButtonPrefab = new GameObject[4];
        ButtonPrefab[0] = GameObject.Find("ButtonPrefabs0");
        ButtonPrefab[1] = GameObject.Find("ButtonPrefabs1");
        ButtonPrefab[2] = GameObject.Find("ButtonPrefabs2");
        ButtonPrefab[3] = GameObject.Find("ButtonPrefabs3");

        GameObject CententGO;
        CententGO = m_ItemButtonsPanelGo[0].transform.Find("Scroll View/Viewport/Content").gameObject;
        for (int i = 0; i < Items.ItemsAtTrain0.Count; i++)
        {
            var Go = GameObject.Instantiate(ButtonPrefab[i], CententGO.transform);
            var Text = Go.GetComponentInChildren<Text>();
            Text.text = Items.ItemsAtTrain0[i].ItemName;
            var Btn = Go.GetComponentInChildren<Button>();
            int Index = i;
            string ItemName = Items.ItemsAtTrain0[Index].ItemName;
            m_StringTextDict[ItemName] = Text;
            Btn.onClick.AddListener(() =>
            {
                if (Text.text.Contains("*"))
                {
                    Text.text = Text.text.Remove(Text.text.Length - 1);
                    EventManager.Trigger(Event.DESELECT_ITEM, new object[] { ItemName });
                }
                else
                {
                    Text.text += "*";
                    EventManager.Trigger(Event.SELECT_ITEM, new object[] { ItemName });
                }
            });
        }

        CententGO = m_ItemButtonsPanelGo[1].transform.Find("Scroll View/Viewport/Content").gameObject;
        for (int i = 0; i < Items.ItemsAtTrain1.Count; i++)
        {
            var Go = GameObject.Instantiate(ButtonPrefab[i], CententGO.transform);
            var Text = Go.GetComponentInChildren<Text>();
            Text.text = Items.ItemsAtTrain1[i].ItemName;
            var Btn = Go.GetComponentInChildren<Button>();
            int Index = i;
            string ItemName = Items.ItemsAtTrain1[Index].ItemName;
            m_StringTextDict[ItemName] = Text;
            Btn.onClick.AddListener(() =>
            {
                if (Text.text.Contains("*"))
                {
                    Text.text = Text.text.Remove(Text.text.Length - 1);
                    EventManager.Trigger(Event.DESELECT_ITEM, new object[] { ItemName });
                }
                else
                {
                    Text.text += "*";
                    EventManager.Trigger(Event.SELECT_ITEM, new object[] { ItemName });
                }
            });
        }

        CententGO = m_ItemButtonsPanelGo[2].transform.Find("Scroll View/Viewport/Content").gameObject;
        for (int i = 0; i < Items.ItemsAtTrain2.Count; i++)
        {
            var Go = GameObject.Instantiate(ButtonPrefab[i], CententGO.transform);
            var Text = Go.GetComponentInChildren<Text>();
            Text.text = Items.ItemsAtTrain2[i].ItemName;
            var Btn = Go.GetComponentInChildren<Button>();
            int Index = i;
            string ItemName = Items.ItemsAtTrain2[Index].ItemName;
            m_StringTextDict[ItemName] = Text;
            Btn.onClick.AddListener(() =>
            {
                if (Text.text.Contains("*"))
                {
                    Text.text = Text.text.Remove(Text.text.Length - 1);
                    EventManager.Trigger(Event.DESELECT_ITEM, new object[] { ItemName });
                }
                else
                {
                    Text.text += "*";
                    EventManager.Trigger(Event.SELECT_ITEM, new object[] { ItemName });
                }
            });
        }

        CententGO = m_ItemButtonsPanelGo[3].transform.Find("Scroll View/Viewport/Content").gameObject;
        for (int i = 0; i < Items.ItemsAtTrain3.Count; i++)
        {
            var Go = GameObject.Instantiate(ButtonPrefab[i], CententGO.transform);
            var Text = Go.GetComponentInChildren<Text>();
            Text.text = Items.ItemsAtTrain3[i].ItemName;
            var Btn = Go.GetComponentInChildren<Button>();
            int Index = i;
            string ItemName = Items.ItemsAtTrain3[Index].ItemName;
            m_StringTextDict[ItemName] = Text;
            Btn.onClick.AddListener(() =>
            {
                if (Text.text.Contains("*"))
                {
                    Text.text = Text.text.Remove(Text.text.Length - 1);
                    EventManager.Trigger(Event.DESELECT_ITEM, new object[] { ItemName });
                }
                else
                {
                    Text.text += "*";
                    EventManager.Trigger(Event.SELECT_ITEM, new object[] { ItemName });
                }
            });
        }

        ButtonPrefab[0].SetActive(false);
        ButtonPrefab[1].SetActive(false);
        ButtonPrefab[2].SetActive(false);
        ButtonPrefab[3].SetActive(false);
    }

    public void ClearItemsButtonMark()
    {
        foreach (var Kv in m_StringTextDict)
        {
            Kv.Value.text = Kv.Key;
        }
    }

    public void SetGamePanelText(string Text)
    {
        if (m_GameText != null)
        {
            m_GameText.text = Text;
        }
    }

    public void SetBackgroundImage(Sprite Background)
    {
        if (m_BackgroundImage != null && Background != null)
        {
            m_BackgroundImage.sprite = Background;
        }
    }

    private void SelectTrainItemsTab(int Index)
    {
        if (Index < 0 && Index > 4)
        {
            return;
        }

        if (Index == 0)
        {
            for (int i = 0; i < m_ItemButtonsPanelGo.Length; i++)
            {
                m_ItemButtonsPanelGo[i].SetActive(false);
                m_TrainIdxTabButtons[i].interactable = true;

            }
            m_ItemButtonsPanelGo[0].SetActive(true);
            m_TrainIdxTabButtons[0].interactable = false;
        }
        else if (Index == 1)
        {
            for (int i = 0; i < m_ItemButtonsPanelGo.Length; i++)
            {
                m_ItemButtonsPanelGo[i].SetActive(false);
                m_TrainIdxTabButtons[i].interactable = true;
            }
            m_ItemButtonsPanelGo[1].SetActive(true);
            m_TrainIdxTabButtons[1].interactable = false;
        }
        else if (Index == 2)
        {
            for (int i = 0; i < m_ItemButtonsPanelGo.Length; i++)
            {
                m_ItemButtonsPanelGo[i].SetActive(false);
                m_TrainIdxTabButtons[i].interactable = true;
            }
            m_ItemButtonsPanelGo[2].SetActive(true);
            m_TrainIdxTabButtons[2].interactable = false;
        }
        else if (Index == 3)
        {
            for (int i = 0; i < m_ItemButtonsPanelGo.Length; i++)
            {
                m_ItemButtonsPanelGo[i].SetActive(false);
                m_TrainIdxTabButtons[i].interactable = true;
            }
            m_ItemButtonsPanelGo[3].SetActive(true);
            m_TrainIdxTabButtons[3].interactable = false;
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

        for (int i = 0; i < m_TrainIdxTabButtons.Length; i++)
        {
            int Index = i;
            m_TrainIdxTabButtons[Index].onClick.AddListener(() =>
            {
                SelectTrainItemsTab(Index);
            });
        }

        m_OKButton.onClick.AddListener(() =>
        {
            EventManager.Trigger(Event.GAME_SUBMIT, null);
        });
    }
}
