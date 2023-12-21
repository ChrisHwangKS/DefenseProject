using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TurretCharacter ������Ʈ�� ������ �Բ� ���Ǿ�� �ϴ� ������Ʈ�� �����մϴ�.
/// TurretDetectArea ������Ʈ�� TurretCharacter ������Ʈ�� �߰��� �� ���� �߰��˴ϴ�.
/// </summary>
[RequireComponent(typeof(TurretDetectArea))]
public class TurretCharacter : BaseCharacter
{
    [Header("ȸ�� ���� ����")]
    public bool m_UseLookatEnemy;

    /// <summary>
    /// �̸������ Material �� ��Ÿ���ϴ�.
    /// </summary>
    private Material _PreviewMaterial;

    /// <summary>
    /// ���� ���� ������Ʈ �Դϴ�.
    /// </summary>
    private TurretDetectArea _DetectArea;

    /// <summary>
    /// ������ �� ��ü���� ��� ��� ����Ʈ�� ���� ������Ƽ�Դϴ�.
    /// </summary>
    protected List<Collider> detectedEnemies { private set; get; } = new();

    /// <summary>
    /// �ͷ� �����Ϳ� ���� ������Ƽ�Դϴ�.
    /// </summary>
    protected TurretData turretData { get; private set; }

    /// <summary>
    /// _DetectArea �� ���� �б� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public TurretDetectArea detectArea => 
        _DetectArea ?? (_DetectArea = GetComponent<TurretDetectArea>());


    protected override void Awake()
    {
        // override : �θ� Ŭ������ Awake �޼��带 �������մϴ�.

        // �θ� Ŭ������ Awake �޼��带 ȣ���մϴ�.
        base.Awake();

        // �� ���� �̺�Ʈ�� �޼��� ���ε�
        detectArea.onEnemyDetected += onEnemyDetected;
    }

    protected virtual void Update()
    {
        // ���� �ٶ󺸴� ����� ����Ѵٸ�
        if(m_UseLookatEnemy)
        {
            // ���� ����� ���� �ٶ󺸵��� �մϴ�.
            LookAtNearestEnemy();
        }
    }


    /// <summary>
    /// ���� �����Ǿ��� ��� ȣ��˴ϴ�
    /// </summary>
    /// <param name="enemyCollider">�� �ö��̴��� ���޵˴ϴ�.</param>
    private void onEnemyDetected(Collider[] enemyCollider)
    {
        // ������ �� ��ü���� ����Ʈ�� ��� ����ϴ�.
        detectedEnemies = new(enemyCollider);

    }

    /// <summary>
    /// ���� ����� �� ��ü�� �ٶ󺸵��� �մϴ�.
    /// </summary>
    private void LookAtNearestEnemy()
    {
        // �ٶ� ��ü�� �������� �ʴ´ٸ� ȣ�� ����
        if (detectedEnemies.Count == 0) return;

        // ���� ����� �� ��ü�� ����ϴ�.
        EnemyCharacter nearestEnemy = detectArea.GetNearestEnemy(detectedEnemies.ToArray());

        // �� ��ü�� �ٶ󺸴� ������ ����մϴ�.
        Vector3 lookDirection = nearestEnemy.transform.position - transform.position;

        // ������ �ٶ󺸵��� �մϴ�.
        LookAt(lookDirection);
    }

    /// <summary>
    /// �ͷ� ĳ���͸� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="previewMaterial">�̸������ ���͸����� �����մϴ�.</param>
    /// <param name="const_TurretData">�� �ͷ��� ����� �ͷ� ������ �����մϴ�.</param>
    public virtual void InitializeTurretCharacter(Material previewMaterial, in TurretData const_TurretData)
    {
        // �̸����� ���͸��� ����
        _PreviewMaterial = Instantiate(previewMaterial);

        // �ͷ� ������ ����
        turretData = const_TurretData;
    }

    /// <summary>
    /// �̸����� ���� ��ȯ�մϴ�.
    /// </summary>
    public void SetPreviewMode()
    {
        meshRenderer.material = _PreviewMaterial;
    }

    /// <summary>
    /// �̸����� ���¸� �����մϴ�.
    /// </summary>
    /// <param name="isInstallable">��ġ ���� ���θ� �����մϴ�.</param>
    public void SetPreviewState(bool isInstallable)
    {
        _PreviewMaterial.SetColor("_Color",(isInstallable ? 
            Constants.TURRET_INSTALL_POSSIBLE_COLOR : 
            Constants.TURRET_INSTALL_IMPOSSIBLE_COLOR));
    }

    /// <summary>
    /// �̸����� ��带 �����մϴ�.
    /// </summary>
    public void FinishPreviewMode()
    {
        meshRenderer.material = originalMaterial;

        // �� ������ �����մϴ�.
        _DetectArea.StartDetectEnemy();
    }

    /// <summary>
    /// ������ ������ �ٶ󺸵��� �մϴ�.
    /// </summary>
    /// <param name="lookDirection"></param>
    public void LookAt(Vector3 lookDirection)
    {
        // �� ������ lookDirection ���� �����մϴ�.
        transform.forward = lookDirection;
    }

}
