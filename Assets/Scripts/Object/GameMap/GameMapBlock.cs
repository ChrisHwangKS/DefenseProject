using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 길찾기에 사용될 노드 클래스
/// </summary>
public class Node
{
    /// <summary>
    /// 노드 위치
    /// </summary>
    public Vector2Int nodePosition { get; set; }

    /// <summary>
    /// 시작 지점부터 드는 비용
    /// </summary>
    public int costFromStart {  get; set; }

    /// <summary>
    /// 도착지까지 예상되는 비용
    /// </summary>
    public int heuristicToGoal { get; set; }

    /// <summary>
    /// 총 비용
    /// </summary>
    public int totalCost => costFromStart + heuristicToGoal;

    /// <summary>
    /// 기반 노드
    /// </summary>
    public Node baseNode { get; set; }
}

/// <summary>
/// 게임 맵을 나타내기 위한 블록 객체를 나타냅니다.
/// </summary>
public class GameMapBlock : MonoBehaviour
{
    /// <summary>
    /// 적 생성 블록
    /// </summary>
    public static GameMapBlock EnemySpawnBlock;

    /// <summary>
    /// 적 목표 블록
    /// </summary>
    public static GameMapBlock EnemyTargetBlock;

    /// <summary>
    /// 블록을 나타내는 Mesh Renderer 컴포넌트 입니다.
    /// </summary>
    private MeshRenderer _MeshRenderer;

    /// <summary>
    /// 배치된 터렛 객체를 나타냅니다.
    /// </summary>
    private TurretCharacter _InTurret;

    /// <summary>
    /// 이 블록에 대한 노드입니다.
    /// </summary>
    public Node node { get; private set; } = new();

    /// <summary>
    /// 맵 블록 타입에 대한 프로퍼티입니다.
    /// </summary>
    public MapBlockType mapBlockType
    {
        get;    // mapBlockType 에 대한 값 가져오기는 외부에서도 가능합니다.
        private set; // mapBlockType 에 대한 값 설정은 해당 클래스 내부에서만 가능
    }

    /// <summary>
    /// 터렛이 존재함을 나타냅니다.
    /// </summary>
    public bool isTurretExist => (_InTurret != null);

    private void Awake()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
    }


    /// <summary>
    /// 맵 블록 타입을 설정합니다.
    /// </summary>
    /// <param name="mapBlockType">설정시킬 맵 블록 타입을 전달합니다.</param>
    private void SetMapBlockType(MapBlockType mapBlockType)
    {
        this.mapBlockType = mapBlockType;
        // this : 자신을 나타내기 위한 키워드

        // 맵 블록 타입이 Default 타입이 아닌 경우
        if (mapBlockType != MapBlockType.Default)
        {
            // 메터리얼의 "_BlockColor" 속성의 값을 붉은 색상으로 설정합니다.
            _MeshRenderer.material.SetColor("_BlockColor", Color.red);
            // - _MeshRenderer.material : _MeshRenderer 의 첫 번째 메터리얼의 복사본을 생성하여 적용시킨 후 반환합니다

            // 적 생성 블록 설정
            if(mapBlockType == MapBlockType.EnemySpawn) EnemySpawnBlock = this;

            // 적 목표 블록 설정
            else if(mapBlockType == MapBlockType.EnemyTarget) EnemyTargetBlock = this;
        }
    }

    /// <summary>
    /// 맵 블록을 초기화합니다.
    /// </summary>
    /// <param name="mapBlockType">맵 블록 타입을 전달합니다.</param>
    public void InitializeMapBlock(MapBlockType mapBlockType)
    {
        // 맵 블록 타입 설정
        SetMapBlockType(mapBlockType);
    }

    /// <summary>
    /// 이 맵 블록 객체가 선택되었을 때 호출됩니다.
    /// </summary>
    public void OnMapBlockSelected()
    {
        _MeshRenderer.material.SetFloat(Constants.MAP_BLOCK_MATPARAM_EMISSION, Constants.MAP_BLOCK_SELECTED_EMISSION);
    }

    /// <summary>
    /// 이 맵 블록 객체가 선택 해제되었을 때 호출됩니다.
    /// </summary>
    public void OnMapBlockUnselected()
    {
        _MeshRenderer.material.SetFloat(Constants.MAP_BLOCK_MATPARAM_EMISSION, Constants.MAP_BLOCK_UNSELECTED_EMISSION);

    }

    /// <summary>
    /// 터렛 캐릭터를 맵 블록에 설정합니다.
    /// </summary>
    /// <param name="turretCharacter">설정시킬 터렛 캐릭터를 전달합니다.</param>
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
