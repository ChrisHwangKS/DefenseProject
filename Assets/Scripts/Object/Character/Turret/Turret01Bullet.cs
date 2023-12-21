using UnityEngine;
using startup.util;

public class Turret01Bullet : MonoBehaviour,
    IObjectPoolable
{
    /// <summary>
    /// �Ѿ��� ���ư� ������ ��Ÿ���ϴ�.
    /// </summary>
    private Vector3 _Direction;

    /// <summary>
    /// �Ѿ� �ʱ� ��ġ�� ��Ÿ���ϴ�.
    /// </summary>
    private Vector3 _InitialPosition;

    /// <summary>
    /// �Ѿ� �ӷ��� ��Ÿ���ϴ�.
    /// </summary>
    private float _Speed;

    private void Update()
    {
        // �浹ü �˻�
        RaycastHit hitResult;

        if(CheckCollision(out hitResult))
        {
            // TODO �ؾ��Ұ�
        }

        // �Ѿ� �߻�
        transform.position += _Direction * 15 * Time.deltaTime;

        // �߻�� ��ġ�� ���� ��ġ�� �Ÿ��� ����մϴ�.
        float distance = Vector3.Distance(_InitialPosition, transform.position);

        // �ִ� �Ÿ��� ��� ��� ������Ʈ�� ��Ȱ��ȭ ��ŵ�ϴ�.
        if(distance > Constants.BULLET_MAX_DISTANCE)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// �浹ü�� Ȯ���մϴ�.
    /// </summary>
    /// <param name="hitResult">�浹ü ���� ����� ��ȯ�մϴ�.</param>
    /// <returns>�浹 ���θ� ��ȯ�մϴ�.</returns>
    private bool CheckCollision(out RaycastHit hitResult)
    {
        Ray ray = new(transform.position,_Direction);


        int layerMask = 1 << LayerMask.NameToLayer("Enemy");

        if(Physics.SphereCast(ray, Constants.BULLET_RADIUS, out hitResult, _Speed * Time.deltaTime, layerMask))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// �Ѿ� ��ü�� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="initialPosition">�ʱ�ȭ�� ��ġ�� �����մϴ�.</param>
    /// <param name="direction">���ư� ������ �����մϴ�.</param>
    /// <param name="bulletSpeed">�Ѿ��� �ӷ��� �����մϴ�.</param>
    public void Initialize(Vector3 initialPosition, Vector3 direction)
    {
        transform.position = _InitialPosition = initialPosition;

        transform.forward = _Direction = direction;

        gameObject.SetActive(true);

        _Speed = Constants.BULLET_SPEED;
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

