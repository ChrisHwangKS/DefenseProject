using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ��ã�⿡ ���� ��� Ŭ����
/// </summary>
public class Node
{
    /// <summary>
    /// ��� ��ġ
    /// </summary>
    public Vector2Int nodePosition { get; set; }

    /// <summary>
    /// ���� �������� ��� ���
    /// </summary>
    public int costFromStart {  get; set; }

    /// <summary>
    /// ���������� ����Ǵ� ���
    /// </summary>
    public int heuristicToGoal { get; set; }

    /// <summary>
    /// �� ���
    /// </summary>
    public int totalCost => costFromStart + heuristicToGoal;

    /// <summary>
    /// ��� ���
    /// </summary>
    public Node baseNode { get; set; }
}

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
    /// ��ġ�� �ͷ� ��ü�� ��Ÿ���ϴ�.
    /// </summary>
    private TurretCharacter _InTurret;

    /// <summary>
    /// �� ��Ͽ� ���� ����Դϴ�.
    /// </summary>
    public Node node { get; private set; } = new();

    /// <summary>
    /// �� ��� Ÿ�Կ� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public MapBlockType mapBlockType
    {
        get;    // mapBlockType �� ���� �� ��������� �ܺο����� �����մϴ�.
        private set; // mapBlockType �� ���� �� ������ �ش� Ŭ���� ���ο����� ����
    }

    /// <summary>
    /// �ͷ��� �������� ��Ÿ���ϴ�.
    /// </summary>
    public bool isTurretExist => (_InTurret != null);

    private void Awake()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
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
    /// �� �� ��� ��ü�� ���õǾ��� �� ȣ��˴ϴ�.
    /// </summary>
    public void OnMapBlockSelected()
    {
        _MeshRenderer.material.SetFloat(Constants.MAP_BLOCK_MATPARAM_EMISSION, Constants.MAP_BLOCK_SELECTED_EMISSION);
    }

    /// <summary>
    /// �� �� ��� ��ü�� ���� �����Ǿ��� �� ȣ��˴ϴ�.
    /// </summary>
    public void OnMapBlockUnselected()
    {
        _MeshRenderer.material.SetFloat(Constants.MAP_BLOCK_MATPARAM_EMISSION, Constants.MAP_BLOCK_UNSELECTED_EMISSION);

    }

    /// <summary>
    /// �ͷ� ĳ���͸� �� ��Ͽ� �����մϴ�.
    /// </summary>
    /// <param name="turretCharacter">������ų �ͷ� ĳ���͸� �����մϴ�.</param>
    public void SetTurret(TurretCharacter turretCharacter)
    {
        _InTurret = turretCharacter;
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
