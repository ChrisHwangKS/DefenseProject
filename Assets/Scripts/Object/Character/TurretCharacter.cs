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
    /// <summary>
    /// 미리보기용 Material 을 나타냅니다.
    /// </summary>
    private Material _PreviewMaterial;

    /// <summary>
    /// 감지 영역 컴포넌트 입니다.
    /// </summary>
    private TurretDetectArea _DetectArea;

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
    }


    public void InitializeTurretCharacter(Material previewMaterial)
    {
        _PreviewMaterial = Instantiate(previewMaterial);
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
    }

}
