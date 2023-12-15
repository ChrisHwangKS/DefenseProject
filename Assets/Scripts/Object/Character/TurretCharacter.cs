using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCharacter : BaseCharacter
{
    /// <summary>
    /// 미리보기용 Material 을 나타냅니다.
    /// </summary>
    private Material _PreviewMaterial;


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
