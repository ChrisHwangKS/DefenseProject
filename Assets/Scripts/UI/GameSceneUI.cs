using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("�÷��̾� ��Ʈ�ѷ�")]
    public PlayerController m_PlayerController;

    [Header("�ͷ�1 ���� ��ư")]
    public Button m_CreateTurret1;

    private void Awake()
    {
        m_CreateTurret1.onClick.AddListener(OnCreateTurret1ButtonClicked);
    }

    /// <summary>
    /// �ͷ�1 ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    private void OnCreateTurret1ButtonClicked()
    {
        m_PlayerController.CreatePreviewTurret(TurretType.Turret1);
    }

}
