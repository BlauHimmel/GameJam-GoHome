using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NameListAnimation : MonoBehaviour
{
    private Text m_NameList;
    private Transform m_TargetPos;
    private Image m_Background1;
    private Image m_Background2;
    private Text m_Text;
    private Button m_ExitButton;
    private Image m_ExitBtnImage;

    void Start ()
    {
        m_NameList = GameObject.Find("NameList").GetComponentInChildren<Text>();
        m_TargetPos = GameObject.Find("TargetPos").transform;

        m_Background1 = GameObject.Find("Background").GetComponentInChildren<Image>();
        m_Background2 = GameObject.Find("Background2").GetComponentInChildren<Image>();

        m_Text = GameObject.Find("Text").GetComponentInChildren<Text>();
        m_ExitButton = GameObject.Find("ExitButton").GetComponentInChildren<Button>();

        m_ExitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        m_ExitBtnImage = m_ExitButton.GetComponent<Image>();
        m_ExitBtnImage.DOFade(0.0f, 0.001f).onComplete += () =>
        {
            m_ExitButton.gameObject.SetActive(false);
        };

        m_Background1.gameObject.SetActive(true);
        m_Background2.DOFade(0.0f, 0.001f);
        m_Background2.gameObject.SetActive(false);
        m_Text.gameObject.SetActive(false);

        m_NameList.text = "制作人员名单\n\n陈良昱 策划\n李晨 策划\n岷溪 音效\n可乐 策划\n小云 随便打打杂之类的\n玉米 程序\n并不存在的美术\n";

        Animation();
    }

    private void Animation()
    {
        m_Text.gameObject.SetActive(true);
        m_Text.text = string.Empty;
        m_Text.DOText("你收拾好行李，挤出了车厢......", 3.0f).onComplete += () =>
        {
            m_Text.text = string.Empty;
            m_Background1.DOFade(0.0f, 2.0f);
            m_Background2.gameObject.SetActive(true);
            m_Background2.DOFade(1.0f, 2.0f).onComplete += () =>
            {
                m_Text.DOText("家里的门开了，你看到了你亲爱的父母......", 3.0f).onComplete += () =>
                {
                    m_Text.text = string.Empty;
                    m_Background2.DOFade(0.0f, 2.0f).onComplete += () =>
                    {
                        m_NameList.transform.DOMove(m_TargetPos.position, 15.0f).SetEase(Ease.Linear).onComplete += () =>
                        {
                            m_ExitButton.gameObject.SetActive(true);
                            m_ExitBtnImage.DOFade(1.0f, 1.0f);
                        };
                    };
                };
            };
        };
    }
}
