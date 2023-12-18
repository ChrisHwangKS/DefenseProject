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
    /// <summary>
    /// �̸������ Material �� ��Ÿ���ϴ�.
    /// </summary>
    private Material _PreviewMaterial;

    /// <summary>
    /// ���� ���� ������Ʈ �Դϴ�.
    /// </summary>
    private TurretDetectArea _DetectArea;

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
    }


    public void InitializeTurretCharacter(Material previewMaterial)
    {
        _PreviewMaterial = Instantiate(previewMaterial);
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
    }

}
