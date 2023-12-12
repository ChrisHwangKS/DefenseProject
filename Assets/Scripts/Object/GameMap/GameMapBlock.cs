using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ���� ���� ��Ÿ���� ���� ��� ��ü�� ��Ÿ���ϴ�.
/// </summary>
public class GameMapBlock : MonoBehaviour
{
    /// <summary>
    /// �� ���� ���
    /// </summary>
    public static GameMapBlock EnemySpawnBlock;

    /// <summary>
    /// �� ��ǥ ���
    /// </summary>
    public static GameMapBlock EnemyTargetBlock;

    /// <summary>
    /// ����� ��Ÿ���� Mesh Renderer ������Ʈ �Դϴ�.
    /// </summary>
    private MeshRenderer _MeshRenderer;

    /// <summary>
    /// �� ��� Ÿ�Կ� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public MapBlockType mapBlockType
    {
        get;    // mapBlockType �� ���� �� ��������� �ܺο����� �����մϴ�.
        private set; // mapBlockType �� ���� �� ������ �ش� Ŭ���� ���ο����� ����
    }

    private void Awake()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// �� ����� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="mapBlockType">�� ��� Ÿ���� �����մϴ�.</param>
    public void InitializeMapBlock(MapBlockType mapBlockType)
    {
        // �� ��� Ÿ�� ����
        SetMapBlockType(mapBlockType);
    }

    /// <summary>
    /// �� ��� Ÿ���� �����մϴ�.
    /// </summary>
    /// <param name="mapBlockType">������ų �� ��� Ÿ���� �����մϴ�.</param>
    private void SetMapBlockType(MapBlockType mapBlockType)
    {
        this.mapBlockType = mapBlockType;
        // this : �ڽ��� ��Ÿ���� ���� Ű����

        // �� ��� Ÿ���� Default Ÿ���� �ƴ� ���
        if (mapBlockType != MapBlockType.Default)
        {
            // ���͸����� "_BlockColor" �Ӽ��� ���� ���� �������� �����մϴ�.
            _MeshRenderer.material.SetColor("_BlockColor", Color.red);
            // - _MeshRenderer.material : _MeshRenderer �� ù ��° ���͸����� ���纻�� �����Ͽ� �����Ų �� ��ȯ�մϴ�

            // �� ���� ��� ����
            if(mapBlockType == MapBlockType.EnemySpawn) EnemySpawnBlock = this;

            // �� ��ǥ ��� ����
            else if(mapBlockType == MapBlockType.EnemyTarget) EnemyTargetBlock = this;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (mapBlockType != MapBlockType.Default)
        {
            Handles.color = Color.white;
            Handles.Label(
                transform.position,
                mapBlockType.ToString()
                );
        }
    }
#endif

}
