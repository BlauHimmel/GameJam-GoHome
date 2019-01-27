using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupUI : MonoBehaviour
{
    private Button m_StartButton;
    private Button m_ExitButton;
    private AudioManager m_AudioManager;

    void Start ()
    {
        m_StartButton = GameObject.Find("StartButton").GetComponentInChildren<Button>();
        m_ExitButton = GameObject.Find("ExitButton").GetComponentInChildren<Button>();
        m_AudioManager = GetComponent<AudioManager>();
        m_AudioManager.BGMPlay("开始菜单bgm");

        m_StartButton.onClick.AddListener(() =>
        {
            m_AudioManager.BGMStop();
            SceneManager.LoadScene("MainScene");
        });

        m_ExitButton.onClick.AddListener(() =>
        {
            m_AudioManager.BGMStop();
            Application.Quit();
        });
    }
}
