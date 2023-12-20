using UnityEngine;
using startup.util;

public class Turret01Bullet : MonoBehaviour,
    IObjectPoolable
{
    /// <summary>
    /// 총알이 날아갈 방향을 나타냅니다.
    /// </summary>
    private Vector3 _Direction;

    private void Update()
    {
        transform.position += _Direction * 15 * Time.deltaTime;
    }

    /// <summary>
    /// 총알 객체를 초기화합니다.
    /// </summary>
    /// <param name="initialPosition">초기화될 위치를 전달합니다.</param>
    /// <param name="direction">날아갈 방향을 전달합니다.</param>
    public void Initialize(Vector3 initialPosition, Vector3 direction)
    {
        transform.position = initialPosition;

        _Direction = direction;
    }

    /// <summary>
    /// 오브젝트가 비활성화 상태라면 재사용 가능하도록 합니다.
    /// </summary>
    /// <returns></returns>
    public bool IsRecyclable()
    {
        return !gameObject.activeSelf;
    }

    

}

