

using System;
using UnityEngine;

/// <summary>
/// 터렛 정보를 나타내기 위한 클래스 입니다.
/// </summary>
[CreateAssetMenu(fileName = "TurretInfo", menuName = "ScriptableObject/TurretInfo")]
public class TurretInfo : ScriptableObject
{
    [Header("터렛 설치 미리보기용 메터리얼")]
    public Material m_TurretPreviewMaterial;


    /// <summary>
    /// 터렛 데이터들을 나타냅니다.
    /// </summary>
    [Header("터렛 데이터들")]
    public TurretData[] m_TurretDatas;
}

/// <summary>
/// 하나의 터렛 데이터를 나타내기 위한 구조체
/// </summary>
[Serializable]
public struct TurretData
{
    /// <summary>
    /// 터렛 프리팹입니다.
    /// </summary>
    public TurretCharacter _TurretPrefabs;
}