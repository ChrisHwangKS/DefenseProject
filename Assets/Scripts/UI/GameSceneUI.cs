using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{

    [Header("�ͷ�1 ���� ��ư")]
    public Button m_CreateTurret1;

    /// <summary>
    /// �ͷ�1 ���� ��ư �̺�Ʈ
    /// </summary>
    public event UnityAction createTurret1ButtonEvent;

    private void Start()
    {
        m_CreateTurret1.onClick.AddListener(createTurret1ButtonEvent);
    }


}
