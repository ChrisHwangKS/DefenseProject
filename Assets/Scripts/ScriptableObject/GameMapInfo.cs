using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 맵 정보를 나타내기 위한 클래스입니다.
/// </summary>
[CreateAssetMenu(fileName = "GameMapInfo", menuName = "ScriptableObject/GameMapInfo")]
public class GameMapInfo : ScriptableObject
{
    /// <summary>
    /// 게임 맵 크기 X 를 나타냅니다
    /// </summary>
    public int mapSizeX;

    /// <summary>
    /// 게임 맵 크기 Y 를 나타냅니다
    /// </summary>
    public int mapSizeY;
}
