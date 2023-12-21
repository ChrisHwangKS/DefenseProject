using UnityEngine;
using startup.util;

public class Turret01Bullet : MonoBehaviour,
    IObjectPoolable
{
    /// <summary>
    /// 총알이 날아갈 방향을 나타냅니다.
    /// </summary>
    private Vector3 _Direction;

    /// <summary>
    /// 총알 초기 위치를 나타냅니다.
    /// </summary>
    private Vector3 _InitialPosition;

    /// <summary>
    /// 총알 속력을 나타냅니다.
    /// </summary>
    private float _Speed;

    private void Update()
    {
        // 충돌체 검사
        RaycastHit hitResult;

        if(CheckCollision(out hitResult))
        {
            // TODO 해야할것
        }

        // 총알 발사
        transform.position += _Direction * 15 * Time.deltaTime;

        // 발사된 위치와 현재 위치의 거리를 계산합니다.
        float distance = Vector3.Distance(_InitialPosition, transform.position);

        // 최대 거리를 벗어난 경우 오브젝트를 비활성화 시킵니다.
        if(distance > Constants.BULLET_MAX_DISTANCE)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 충돌체를 확인합니다.
    /// </summary>
    /// <param name="hitResult">충돌체 감지 결과를 반환합니다.</param>
    /// <returns>충돌 여부를 반환합니다.</returns>
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
    /// 총알 객체를 초기화합니다.
    /// </summary>
    /// <param name="initialPosition">초기화될 위치를 전달합니다.</param>
    /// <param name="direction">날아갈 방향을 전달합니다.</param>
    /// <param name="bulletSpeed">총알의 속력을 전달합니다.</param>
    public void Initialize(Vector3 initialPosition, Vector3 direction)
    {
        transform.position = _InitialPosition = initialPosition;

        transform.forward = _Direction = direction;

        gameObject.SetActive(true);

        _Speed = Constants.BULLET_SPEED;
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

