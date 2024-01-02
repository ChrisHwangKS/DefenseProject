using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적의 이동 기능을 나타내기 위한 컴포넌트입니다.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("이동 속력")]
    public float m_MoveSpeed;

    /// <summary>
    /// 이동에 사용될 경유지 목록을 나타냅니다.
    /// </summary>
    private List<GameMapBlock> m_Stopovers;

    private void Start()
    {
        // 경유지를 얻습니다.
        m_Stopovers = GameManager.instance.m_Stopovers;
    }

    private void Update()
    {
        MoveToTarget();
    }

    /// <summary>
    /// 목표 위치로 이동합니다.
    /// </summary>
    private void MoveToTarget()
    {
        // TODO Sample
        Vector3 targetPosition = GameMapBlock.EnemyTargetBlock.transform.position;

        // 현재 위치를 얻습니다.
        Vector3 currentPosition = transform.position;

        // 목표 위치로 향하는 방향을 계산합니다.
        Vector3 moveDirection = (targetPosition - currentPosition).normalized;

        // 다음 위치를 계산합니다.
        Vector3 nextPosition = currentPosition + (moveDirection * m_MoveSpeed * Time.deltaTime);

        // 다음 위치를 설정합니다.
        transform.position = nextPosition;
    }
}
