using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    /// <summary>
    /// Material ������ ��Ÿ���ϴ�.
    /// </summary>
    private Material _OriginalMaterial;

    /// <summary>
    /// MeshRenderer ������Ʈ�� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public MeshRenderer meshRenderer {  get; private set; }

    /// <summary>
    /// _OriginalMaterial �� ���� �б� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public Material originalMaterial => _OriginalMaterial;

    protected virtual void Awake()
    {
        // protected : �θ�� �ڽ� ���̿����� ������ ����ϴ� ����������
        // virtual : �ڽ� Ŭ�������� �޼��带 ������ �����ϵ��� �����մϴ�.

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        // GetComponentInChildren<T>() : T ������ ������Ʈ�� �ڽĿ�����Ʈ���� ã���ϴ�.

        // �⺻ ���͸����� ����ϴ�.
        _OriginalMaterial = meshRenderer.material;
        meshRenderer.material = _OriginalMaterial;
    }
}
