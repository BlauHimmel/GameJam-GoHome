using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class StartupUI : MonoBehaviour
{
    private Button m_StartButton;
    private Button m_ExitButton;

    void Start ()
    {
        m_StartButton = GameObject.Find("StartButton").GetComponentInChildren<Button>();
        m_ExitButton = GameObject.Find("ExitButton").GetComponentInChildren<Button>();

        m_StartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

        m_ExitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
