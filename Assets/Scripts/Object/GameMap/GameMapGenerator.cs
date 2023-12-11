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

    [Header("ī�޶� ����")]
    public Camera m_Camera;



    private void Awake()
    {
        // ���� �� ����
        GenerateGameMap(m_SampleGameMapInfo);

        // ī�޶� ��ġ �ʱ�ȭ
        InitializeCameraPosition();
    }

    /// <summary>
    /// ���� ���� �����մϴ�
    /// </summary>
    /// <param name="gameMapInfo">���� �� ���� ��ü�� �����մϴ�.</param>
    private void GenerateGameMap(GameMapInfo gameMapInfo)
    {
        // ��ü �� ũ�⸦ ��Ÿ���ϴ�.
        Vector2 mapSizeXY = new Vector2(gameMapInfo.mapSizeX, gameMapInfo.mapSizeY) * Constants.MAP_BLOCK_SIZE;

        // �� ��� ���� ���� ��ǥ�� �����մϴ�.
        Vector2 startGeneratinXY = (mapSizeXY * -0.5f) + (Vector2.one * Constants.MAP_BLOCK_SIZE * 0.5f);

        for(int x =0; x< gameMapInfo.mapSizeX; ++x)
        {
            for(int y= 0; y< gameMapInfo.mapSizeY; ++y) 
            {
                // �� ����� ������ ��ġ�� �����մϴ�.
                Vector2 blockPosition = startGeneratinXY + (Constants.MAP_BLOCK_SIZE * new Vector2(x, y));

                // �� ����� �����մϴ�.
                GameObject blockObject = Instantiate(m_GameBlockPrefab);

                // ��ġ�� �����մϴ�.
                blockObject.transform.position = new Vector3(blockPosition.x, 0 ,blockPosition.y);
            }
        }

        // ���� ���� ��ġ


        // ���� ��ǥ ����


    }


    /// <summary>
    /// ī�޶� ��ġ�� �ʱ�ȭ�մϴ�.
    /// </summary>
    private void InitializeCameraPosition()
    {
        // �� Y ũ�⸦ �̿��Ͽ� ī�޶� ���̸� ��ȯ�մϴ�.
        float GetHeightFromMapY()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeY * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // �� X ũ�⸦ �̿��Ͽ� ī�޶� ���̸� ��ȯ�մϴ�.
        float GetHeightFromMapX()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeX * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // ī�޶� �Ʒ��� �ٶ󺸵��� �մϴ�.
        m_Camera.transform.eulerAngles = Vector3.right * 90.0f;

        float heightFromMapY = GetHeightFromMapY();
        float heightFromMapX = GetHeightFromMapX();

        // �� ���� ���̰��� ����Ͽ� ��� ���� ���� �� �ֵ��� �մϴ�.
        float cameraHeight = (heightFromMapY > heightFromMapX) ? heightFromMapY : heightFromMapX;

        // ī�޶� ���̸� �����մϴ�.
        m_Camera.transform.position = Vector3.up * (cameraHeight + Constants.CAMERA_HEIGHT_TERM);






    }
}
