using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TurretCharacter 컴포넌트와 무조건 함께 사용되어야 하는 컴포넌트를 지정합니다.
/// TurretDetectArea 컴포넌트는 TurretCharacter 컴포넌트를 추가할 때 같이 추가됩니다.
/// </summary>
[RequireComponent(typeof(TurretDetectArea))]
public class TurretCharacter : BaseCharacter
{
    [Header("회전 가능 여부")]
    public bool m_UseLookatEnemy;

    /// <summary>
    /// 미리보기용 Material 을 나타냅니다.
    /// </summary>
    private Material _PreviewMaterial;

    /// <summary>
    /// 감지 영역 컴포넌트 입니다.
    /// </summary>
    private TurretDetectArea _DetectArea;

    /// <summary>
    /// 감지된 적 객체들을 모두 담는 리스트에 대한 프로퍼티입니다.
    /// </summary>
    protected List<Collider> detectedEnemies { private set; get; } = new();

    /// <summary>
    /// 터렛 데이터에 대한 프로퍼티입니다.
    /// </summary>
    protected TurretData turretData { get; private set; }

    /// <summary>
    /// _DetectArea 에 대한 읽기 전용 프로퍼티입니다.
    /// </summary>
    public TurretDetectArea detectArea => 
        _DetectArea ?? (_DetectArea = GetComponent<TurretDetectArea>());


    protected override void Awake()
    {
        // override : 부모 클래스의 Awake 메서드를 재정의합니다.

        // 부모 클래스의 Awake 메서드를 호출합니다.
        base.Awake();

        // 적 감지 이벤트에 메서드 바인딩
        detectArea.onEnemyDetected += onEnemyDetected;
    }

    protected virtual void Update()
    {
        // 적을 바라보는 기능을 사용한다면
        if(m_UseLookatEnemy)
        {
            // 가장 가까운 적을 바라보도록 합니다.
            LookAtNearestEnemy();
        }
    }


    /// <summary>
    /// 적이 감지되었을 경우 호출됩니다
    /// </summary>
    /// <param name="enemyCollider">적 컬라이더가 전달됩니다.</param>
    private void onEnemyDetected(Collider[] enemyCollider)
    {
        // 감지된 적 객체들을 리스트에 모두 담습니다.
        detectedEnemies = new(enemyCollider);

    }

    /// <summary>
    /// 가장 가까운 적 객체를 바라보도록 합니다.
    /// </summary>
    private void LookAtNearestEnemy()
    {
        // 바라볼 객체가 존재하지 않는다면 호출 종료
        if (detectedEnemies.Count == 0) return;

        // 가장 가까운 적 객체를 얻습니다.
        EnemyCharacter nearestEnemy = detectArea.GetNearestEnemy(detectedEnemies.ToArray());

        // 적 객체를 바라보는 방향을 계산합니다.
        Vector3 lookDirection = nearestEnemy.transform.position - transform.position;

        // 방향을 바라보도록 합니다.
        LookAt(lookDirection);
    }

    /// <summary>
    /// 터렛 캐릭터를 초기화합니다.
    /// </summary>
    /// <param name="previewMaterial">미리보기용 메터리얼을 전달합니다.</param>
    /// <param name="const_TurretData">이 터렛이 사용할 터렛 정보를 전달합니다.</param>
    public virtual void InitializeTurretCharacter(Material previewMaterial, in TurretData const_TurretData)
    {
        // 미리보기 메터리얼 설정
        _PreviewMaterial = Instantiate(previewMaterial);

        // 터렛 데이터 설정
        turretData = const_TurretData;
    }

    /// <summary>
    /// 미리보기 모드로 전환합니다.
    /// </summary>
    public void SetPreviewMode()
    {
        meshRenderer.material = _PreviewMaterial;
    }

    /// <summary>
    /// 미리보기 상태를 설정합니다.
    /// </summary>
    /// <param name="isInstallable">설치 가능 여부를 전달합니다.</param>
    public void SetPreviewState(bool isInstallable)
    {
        _PreviewMaterial.SetColor("_Color",(isInstallable ? 
            Constants.TURRET_INSTALL_POSSIBLE_COLOR : 
            Constants.TURRET_INSTALL_IMPOSSIBLE_COLOR));
    }

    /// <summary>
    /// 미리보기 모드를 종료합니다.
    /// </summary>
    public void FinishPreviewMode()
    {
        meshRenderer.material = originalMaterial;

        // 적 감지를 시작합니다.
        _DetectArea.StartDetectEnemy();
    }

    /// <summary>
    /// 전달한 방향을 바라보도록 합니다.
    /// </summary>
    /// <param name="lookDirection"></param>
    public void LookAt(Vector3 lookDirection)
    {
        // 앞 방향을 lookDirection 으로 설정합니다.
        transform.forward = lookDirection;
    }

}
