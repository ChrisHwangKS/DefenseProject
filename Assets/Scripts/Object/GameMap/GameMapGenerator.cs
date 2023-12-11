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

    [Header("카메라 변수")]
    public Camera m_Camera;



    private void Awake()
    {
        // 게임 맵 생성
        GenerateGameMap(m_SampleGameMapInfo);

        // 카메라 위치 초기화
        InitializeCameraPosition();
    }

    /// <summary>
    /// 게임 맵을 생성합니다
    /// </summary>
    /// <param name="gameMapInfo">게임 맵 정보 객체를 전달합니다.</param>
    private void GenerateGameMap(GameMapInfo gameMapInfo)
    {
        // 전체 맵 크기를 나타냅니다.
        Vector2 mapSizeXY = new Vector2(gameMapInfo.mapSizeX, gameMapInfo.mapSizeY) * Constants.MAP_BLOCK_SIZE;

        // 맵 블록 생성 시작 좌표를 저장합니다.
        Vector2 startGeneratinXY = (mapSizeXY * -0.5f) + (Vector2.one * Constants.MAP_BLOCK_SIZE * 0.5f);

        for(int x =0; x< gameMapInfo.mapSizeX; ++x)
        {
            for(int y= 0; y< gameMapInfo.mapSizeY; ++y) 
            {
                // 맵 블록의 생성될 위치를 결정합니다.
                Vector2 blockPosition = startGeneratinXY + (Constants.MAP_BLOCK_SIZE * new Vector2(x, y));

                // 맵 블록을 생성합니다.
                GameObject blockObject = Instantiate(m_GameBlockPrefab);

                // 위치를 설정합니다.
                blockObject.transform.position = new Vector3(blockPosition.x, 0 ,blockPosition.y);
            }
        }

        // 적의 생성 위치


        // 적의 목표 지점


    }


    /// <summary>
    /// 카메라 위치를 초기화합니다.
    /// </summary>
    private void InitializeCameraPosition()
    {
        // 맵 Y 크기를 이용하여 카메라 높이를 반환합니다.
        float GetHeightFromMapY()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeY * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // 맵 X 크기를 이용하여 카메라 높이를 반환합니다.
        float GetHeightFromMapX()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeX * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // 카메라가 아래를 바라보도록 합니다.
        m_Camera.transform.eulerAngles = Vector3.right * 90.0f;

        float heightFromMapY = GetHeightFromMapY();
        float heightFromMapX = GetHeightFromMapX();

        // 더 높은 높이값을 사용하여 모든 맵이 보일 수 있도록 합니다.
        float cameraHeight = (heightFromMapY > heightFromMapX) ? heightFromMapY : heightFromMapX;

        // 카메라 높이를 설정합니다.
        m_Camera.transform.position = Vector3.up * (cameraHeight + Constants.CAMERA_HEIGHT_TERM);






    }
}
