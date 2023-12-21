using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// TurretCharacter 를 기반으로 터렛 01 클래스를 정의합니다.
/// sealed : 상속 봉인 키워드
/// 더이상 Turret01 클래스를 상속받아 확장하지 못하도록 상속기능을 봉인합니다.
/// </summary>
[RequireComponent(typeof(Turret01Attack))]
public sealed class Turret01 : TurretCharacter
{
    /// <summary>
    /// 터렛 공격 컴포넌트입니다.
    /// </summary>
    private Turret01Attack _TurretAttack;

    protected override void Awake()
    {
        // 부모 메서드 호출
        base.Awake();

        // Turret01Attack 컴포넌트를 얻어 변수에 저장합니다.
        _TurretAttack = GetComponent<Turret01Attack>();

        // 적 감지 이벤트에 메서드를 바인딩합니다.
        detectArea.onEnemyDetected += OnEnemyDetected;
    }

    public override void InitializeTurretCharacter(Material previewMaterial, in TurretData const_TurretData)
    {
        base.InitializeTurretCharacter(previewMaterial, const_TurretData);

        // 공격 객체 초기화
        _TurretAttack.Initialize(turretData._AttackDelay);
    }

    /// <summary>
    /// 적이 감지되었을 경우 호출되는 메서드입니다.
    /// </summary>
    /// <param name="detectedEnemies">감지된 적들의 Collider 객체들이 전달됩니다.</param>
    private void OnEnemyDetected(Collider[] detectedEnemies)
    {
        // 가장 가까운 적 객체를 얻습니다.
        EnemyCharacter nearestEnemy = detectArea.GetNearestEnemy(detectedEnemies);

        // 적을 향하는 방향을 얻습니다.
        Vector3 directionToEnemy = (nearestEnemy.transform.position - transform.position).normalized;

        // 적 방향으로 공격합니다.
        _TurretAttack.Attack(directionToEnemy);
    }
}
