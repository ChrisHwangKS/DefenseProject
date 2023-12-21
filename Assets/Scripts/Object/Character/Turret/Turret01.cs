using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// TurretCharacter �� ������� �ͷ� 01 Ŭ������ �����մϴ�.
/// sealed : ��� ���� Ű����
/// ���̻� Turret01 Ŭ������ ��ӹ޾� Ȯ������ ���ϵ��� ��ӱ���� �����մϴ�.
/// </summary>
[RequireComponent(typeof(Turret01Attack))]
public sealed class Turret01 : TurretCharacter
{
    /// <summary>
    /// �ͷ� ���� ������Ʈ�Դϴ�.
    /// </summary>
    private Turret01Attack _TurretAttack;

    protected override void Awake()
    {
        // �θ� �޼��� ȣ��
        base.Awake();

        // Turret01Attack ������Ʈ�� ��� ������ �����մϴ�.
        _TurretAttack = GetComponent<Turret01Attack>();

        // �� ���� �̺�Ʈ�� �޼��带 ���ε��մϴ�.
        detectArea.onEnemyDetected += OnEnemyDetected;
    }

    public override void InitializeTurretCharacter(Material previewMaterial, in TurretData const_TurretData)
    {
        base.InitializeTurretCharacter(previewMaterial, const_TurretData);

        // ���� ��ü �ʱ�ȭ
        _TurretAttack.Initialize(turretData._AttackDelay);
    }

    /// <summary>
    /// ���� �����Ǿ��� ��� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    /// <param name="detectedEnemies">������ ������ Collider ��ü���� ���޵˴ϴ�.</param>
    private void OnEnemyDetected(Collider[] detectedEnemies)
    {
        // ���� ����� �� ��ü�� ����ϴ�.
        EnemyCharacter nearestEnemy = detectArea.GetNearestEnemy(detectedEnemies);

        // ���� ���ϴ� ������ ����ϴ�.
        Vector3 directionToEnemy = (nearestEnemy.transform.position - transform.position).normalized;

        // �� �������� �����մϴ�.
        _TurretAttack.Attack(directionToEnemy);
    }
}
