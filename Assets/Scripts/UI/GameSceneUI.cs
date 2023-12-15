using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{

    [Header("터렛1 생성 버튼")]
    public Button m_CreateTurret1;

    /// <summary>
    /// 터렛1 생성 버튼 이벤트
    /// </summary>
    public event UnityAction createTurret1ButtonEvent;

    private void Start()
    {
        m_CreateTurret1.onClick.AddListener(createTurret1ButtonEvent);
    }


}
