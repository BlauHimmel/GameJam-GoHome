using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NameListAnimation : MonoBehaviour
{
    private Text m_NameList;
    private Transform m_TargetPos;

	void Start ()
    {
        m_NameList = GameObject.Find("NameList").GetComponentInChildren<Text>();
        m_TargetPos = GameObject.Find("TargetPos").transform;

        m_NameList.transform.DOMove(m_TargetPos.position, 15.0f).SetEase(Ease.Linear);
    }
}
