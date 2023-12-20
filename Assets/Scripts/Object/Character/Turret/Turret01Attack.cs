using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using startup.util;

/// <summary>
/// 터렛01 타입의 공격 기능을 나타내기 위한 클래스입니다.
/// </summary>
public sealed class Turret01Attack : MonoBehaviour
{
    [Header("터렛01 총알 프리팹")]
    public Turret01Bullet m_BulletPrefab;

    /// <summary>
    /// 총알 풀 객체입니다.
    /// </summary>
    private ObjectPool<Turret01Bullet> _BulletPool;


    private void Awake()
    {
        // 총알 풀 객체 생성
        _BulletPool = new();
    }

    /// <summary>
    /// 공격합니다.
    /// </summary>
    /// <param name="direction">총알 발사시킬 방향을 전달합니다.</param>
    public void Attack(Vector3 direction)
    {
        // 재사용 가능한 총알 객체를 얻습니다.
        Turret01Bullet bullet = _BulletPool.GetRecycleObject();

        // 재사용 가능한 총알 객체가 존재하지 않는다면
        if(bullet == null)
        {
            // 총알 객체 생성
            bullet = Instantiate(m_BulletPrefab);

            // 생성된 총알 객체를 풀에 등록합니다.
            _BulletPool.RegisterRecyclableObject(bullet);
        }

        // 총알 초기화
        bullet.Initialize(transform.position, direction);

    }


}
