using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    /// 맵 블록 타입에 대한 프로퍼티입니다.
    /// </summary>
    public MapBlockType mapBlockType
    {
        get;    // mapBlockType 에 대한 값 가져오기는 외부에서도 가능합니다.
        private set; // mapBlockType 에 대한 값 설정은 해당 클래스 내부에서만 가능
    }

    private void Awake()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
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
