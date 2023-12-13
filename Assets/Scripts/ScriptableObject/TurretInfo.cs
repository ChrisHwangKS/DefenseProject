

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
}

/// <summary>
/// �ϳ��� �ͷ� �����͸� ��Ÿ���� ���� ����ü
/// </summary>
[Serializable]
public struct TurretData
{
    /// <summary>
    /// �ͷ� �������Դϴ�.
    /// </summary>
    public TurretCharacter _TurretPrefabs;
}