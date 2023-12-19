using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ͷ�01 Ÿ���� ���� ����� ��Ÿ���� ���� Ŭ�����Դϴ�.
/// </summary>
public sealed class Turret01Attack : MonoBehaviour
{
    // TODO ���� �ʵ�
    private Vector3 _TempDirection;

    /// <summary>
    /// �����մϴ�.
    /// </summary>
    /// <param name="direction">�Ѿ� �߻��ų ������ �����մϴ�.</param>
    public void Attack(Vector2 direction)
    {
        Debug.Log("����");

        // ������ ��� �����մϴ�.
        _TempDirection = direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            transform.position, 
            transform.position + (_TempDirection * 3.0f));
    }
}
