using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 맵 생성을 위한 컴포넌트
/// </summary>
public class GameMapGenerator : MonoBehaviour
{
    // TODO Test
    public GameMapInfo m_SampleGameMapInfo;

    [Header("게임 맵 블록 프리팹")]
    public GameObject m_GameBlockPrefab;

    private void Awake()
    {
        // 게임 맵 생성
        GenerateGameMap(m_SampleGameMapInfo);
    }

    /// <summary>
    /// 게임 맵을 생성합니다
    /// </summary>
    /// <param name="gameMapInfo">게임 맵 정보 객체를 전달합니다.</param>
    private void GenerateGameMap(GameMapInfo gameMapInfo)
    {
        for(int x =0; x< gameMapInfo.mapSizeX; ++x)
        {
            for(int y= 0; y< gameMapInfo.mapSizeY; ++y) 
            {
                GameObject blockObject = Instantiate(m_GameBlockPrefab);
                blockObject.transform.position = new Vector3(x, 0 ,y);
            }
        }
    }
}
