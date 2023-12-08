using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �� ������ ���� ������Ʈ
/// </summary>
public class GameMapGenerator : MonoBehaviour
{
    // TODO Test
    public GameMapInfo m_SampleGameMapInfo;

    [Header("���� �� ��� ������")]
    public GameObject m_GameBlockPrefab;

    private void Awake()
    {
        // ���� �� ����
        GenerateGameMap(m_SampleGameMapInfo);
    }

    /// <summary>
    /// ���� ���� �����մϴ�
    /// </summary>
    /// <param name="gameMapInfo">���� �� ���� ��ü�� �����մϴ�.</param>
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
