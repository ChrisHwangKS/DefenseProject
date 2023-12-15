

using System;
using UnityEngine;

/// <summary>
/// �ͷ� ������ ��Ÿ���� ���� Ŭ���� �Դϴ�.
/// </summary>
[CreateAssetMenu(fileName = "TurretInfo", menuName = "ScriptableObject/TurretInfo")]
public class TurretInfo : ScriptableObject
{
    [Header("�ͷ� ��ġ �̸������ ���͸���")]
    public Material m_TurretPreviewMaterial;


    /// <summary>
    /// �ͷ� �����͵��� ��Ÿ���ϴ�.
    /// </summary>
    [Header("�ͷ� �����͵�")]
    public TurretData[] m_TurretDatas;


    /// <summary>
    /// Ư�� �ͷ� �����͸� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="turretType">������ ��ȯ���� �ͷ� Ÿ���� �����մϴ�.</param>
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
/// �ϳ��� �ͷ� �����͸� ��Ÿ���� ���� ����ü
/// </summary>
[Serializable]
public struct TurretData
{
    public TurretType turretType;

    /// <summary>
    /// �ͷ� �������Դϴ�.
    /// </summary>
    public TurretCharacter turretPrefab;
}