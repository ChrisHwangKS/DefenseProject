using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 터렛01 타입의 공격 기능을 나타내기 위한 클래스입니다.
/// </summary>
public sealed class Turret01Attack : MonoBehaviour
{
    // TODO 샘플 필드
    private Vector3 _TempDirection;

    /// <summary>
    /// 공격합니다.
    /// </summary>
    /// <param name="direction">총알 발사시킬 방향을 전달합니다.</param>
    public void Attack(Vector2 direction)
    {
        Debug.Log("공격");

        // 방향을 잠시 저장합니다.
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
