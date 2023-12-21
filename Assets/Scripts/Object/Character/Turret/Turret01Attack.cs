using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using startup.util;

/// <summary>
/// �ͷ�01 Ÿ���� ���� ����� ��Ÿ���� ���� Ŭ�����Դϴ�.
/// </summary>
public sealed class Turret01Attack : MonoBehaviour
{
    [Header("�ͷ�01 �Ѿ� ������")]
    public Turret01Bullet m_BulletPrefab;

    [Header("�ѱ� ��ġ")]
    public Transform m_FireTransform;

    /// <summary>
    /// ���� �����̸� ��Ÿ���ϴ�.
    /// </summary>
    private float _AttackDelay;

    /// <summary>
    /// ������ ���� �ð��� ��Ÿ���ϴ�.
    /// </summary>
    private float _LastAttackTime;

    /// <summary>
    /// �Ѿ� Ǯ ��ü�Դϴ�.
    /// </summary>
    private static ObjectPool<Turret01Bullet> _BulletPool;


    private void Awake()
    {
        // �Ѿ� Ǯ ��ü ����
        _BulletPool = new();
    }

    /// <summary>
    /// ���� ��ü�� �ʱ�ȭ �մϴ�.
    /// </summary>
    /// <param name="attackDelay">���� �����̸� �����մϴ�.</param>
    public void Initialize(float attackDelay)
    {
        _AttackDelay = attackDelay;
    }

    /// <summary>
    /// �����մϴ�.
    /// </summary>
    /// <param name="direction">�Ѿ� �߻��ų ������ �����մϴ�.</param>
    public void Attack(Vector3 direction)
    {
        // ���� �����̰� ������ �ʾҴٸ�
        if (Time.time - _LastAttackTime < _AttackDelay) return;

        // ������ ���� �ð� ����
        _LastAttackTime = Time.time; 

        // ���� ������ �Ѿ� ��ü�� ����ϴ�.
        Turret01Bullet bullet = _BulletPool.GetRecycleObject();

        // ���� ������ �Ѿ� ��ü�� �������� �ʴ´ٸ�
        if(bullet == null)
        {
            // �Ѿ� ��ü ����
            bullet = Instantiate(m_BulletPrefab);

            // ������ �Ѿ� ��ü�� Ǯ�� ����մϴ�.
            _BulletPool.RegisterRecyclableObject(bullet);
        }

        // �Ѿ� �ʱ�ȭ
        bullet.Initialize(m_FireTransform.position, direction);

    }


}
