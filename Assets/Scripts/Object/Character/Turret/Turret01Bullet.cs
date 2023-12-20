using UnityEngine;
using startup.util;

public class Turret01Bullet : MonoBehaviour,
    IObjectPoolable
{
    /// <summary>
    /// �Ѿ��� ���ư� ������ ��Ÿ���ϴ�.
    /// </summary>
    private Vector3 _Direction;

    private void Update()
    {
        transform.position += _Direction * 15 * Time.deltaTime;
    }

    /// <summary>
    /// �Ѿ� ��ü�� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="initialPosition">�ʱ�ȭ�� ��ġ�� �����մϴ�.</param>
    /// <param name="direction">���ư� ������ �����մϴ�.</param>
    public void Initialize(Vector3 initialPosition, Vector3 direction)
    {
        transform.position = initialPosition;

        _Direction = direction;
    }

    /// <summary>
    /// ������Ʈ�� ��Ȱ��ȭ ���¶�� ���� �����ϵ��� �մϴ�.
    /// </summary>
    /// <returns></returns>
    public bool IsRecyclable()
    {
        return !gameObject.activeSelf;
    }

    

}

