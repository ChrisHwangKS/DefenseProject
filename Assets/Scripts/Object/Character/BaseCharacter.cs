using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    /// <summary>
    /// Material 원본을 나타냅니다.
    /// </summary>
    private Material _OriginalMaterial;

    /// <summary>
    /// MeshRenderer 컴포넌트에 대한 프로퍼티입니다.
    /// </summary>
    public MeshRenderer meshRenderer {  get; private set; }

    /// <summary>
    /// _OriginalMaterial 에 대한 읽기 전용 프로퍼티입니다.
    /// </summary>
    public Material originalMaterial => _OriginalMaterial;

    protected virtual void Awake()
    {
        // protected : 부모와 자식 사이에서만 접근을 허용하는 접근한정자
        // virtual : 자식 클래스에서 메서드를 재정의 가능하도록 설정합니다.

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        // GetComponentInChildren<T>() : T 형식의 컴포넌트를 자식오브젝트에서 찾습니다.

        // 기본 메터리얼을 얻습니다.
        _OriginalMaterial = meshRenderer.material;
        meshRenderer.material = _OriginalMaterial;
    }
}
