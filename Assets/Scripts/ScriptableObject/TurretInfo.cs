

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


    /// <summary>
    /// 특정 터렛 데이터를 반환합니다.
    /// </summary>
    /// <param name="turretType">정보를 반환받을 터렛 타입을 전달합니다.</param>
    /// <returns></returns>
    public TurretData? GetTurretData(TurretType turretType)
    {
        foreach(TurretData turretData in m_TurretDatas)
        {
            if (turretData.turretType == turretType)
            {
                return turretData;
            }
        }

        return null;
    }
}

/// <summary>
/// 하나의 터렛 데이터를 나타내기 위한 구조체
/// </summary>
[Serializable]
public struct TurretData
{
    public TurretType turretType;

    /// <summary>
    /// 터렛 프리팹입니다.
    /// </summary>
    public TurretCharacter turretPrefab;
}