using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �̵� ����� ��Ÿ���� ���� ������Ʈ�Դϴ�.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("�̵� �ӷ�")]
    public float m_MoveSpeed;

    /// <summary>
    /// �̵��� ���� ������ ����� ��Ÿ���ϴ�.
    /// </summary>
    private List<GameMapBlock> m_Stopovers;

    private void Start()
    {
        // �������� ����ϴ�.
        m_Stopovers = GameManager.instance.m_Stopovers;
    }

    private void Update()
    {
        MoveToTarget();
    }

    /// <summary>
    /// ��ǥ ��ġ�� �̵��մϴ�.
    /// </summary>
    private void MoveToTarget()
    {
        // TODO Sample
        Vector3 targetPosition = GameMapBlock.EnemyTargetBlock.transform.position;

        // ���� ��ġ�� ����ϴ�.
        Vector3 currentPosition = transform.position;

        // ��ǥ ��ġ�� ���ϴ� ������ ����մϴ�.
        Vector3 moveDirection = (targetPosition - currentPosition).normalized;

        // ���� ��ġ�� ����մϴ�.
        Vector3 nextPosition = currentPosition + (moveDirection * m_MoveSpeed * Time.deltaTime);

        // ���� ��ġ�� �����մϴ�.
        transform.position = nextPosition;
    }
}
