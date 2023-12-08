using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �� ������ ��Ÿ���� ���� Ŭ�����Դϴ�.
/// </summary>
[CreateAssetMenu(fileName = "GameMapInfo", menuName = "ScriptableObject/GameMapInfo")]
public class GameMapInfo : ScriptableObject
{
    /// <summary>
    /// ���� �� ũ�� X �� ��Ÿ���ϴ�
    /// </summary>
    public int mapSizeX;

    /// <summary>
    /// ���� �� ũ�� Y �� ��Ÿ���ϴ�
    /// </summary>
    public int mapSizeY;
}
