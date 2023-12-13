using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("플레이어 컨트롤러")]
    public PlayerController m_PlayerController;

    [Header("터렛1 생성 버튼")]
    public Button m_CreateTurret1;

    private void Awake()
    {
        m_CreateTurret1.onClick.AddListener(OnCreateTurret1ButtonClicked);
    }

    /// <summary>
    /// 터렛1 생성 버튼 클릭 시 호출되는 메서드입니다.
    /// </summary>
    private void OnCreateTurret1ButtonClicked()
    {
        m_PlayerController.CreatePreviewTurret(TurretType.Turret1);
    }

}
