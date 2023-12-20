using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetectArea : MonoBehaviour
{
    [Header("감지 반경")]
    public float m_DetectRadius;

    [Header("감지 반경 오프셋")]
    public Vector3 m_AreaOffset;

    /// <summary>
    /// 적 감지를 허용합니다.
    /// </summary>
    private bool _AllowDetectEnemy;

    /// <summary>
    /// 적이 감지되었을 경우 발생시킬 이벤트입니다.
    /// Collider[] detectedEnemyColliders : 감지된 적 컬라이더 객체들이 전달됩니다.
    /// </summary>
    public event Action<Collider[] /*detectedEnemyColliders*/> onEnemyDetected;

    private void Update()
    {
        // 적 감지가 허용된 경우 적 감지를 진행합니다.
        if (_AllowDetectEnemy) DoDetect();
    }

    /// <summary>
    /// 적을 감지합니다.
    /// </summary>
    private void DoDetect()
    {
        // 적 오브젝트 레이어 마스크를 얻습니다.
        int layerMask = 1 << LayerMask.NameToLayer(Constants.ENEMY_LAYER_NAME);

        // 터렛 위치에서 m_DetectRadius 반경 만큼의 영역 내에서 적 오브젝트를 감지합니다.
        Collider[] detectedEnemy = Physics.OverlapSphere(
            transform.position + m_AreaOffset,
            m_DetectRadius,
            layerMask);

        // 터렛에서 가장 가까운 적을 고릅니다.
        Collider nearestEnemy = null;

        // 하나 이상의 적이 감지된 경우
        if(detectedEnemy.Length > 0)
        {
            // 적 감지 이벤트를 발생시킵니다.
            onEnemyDetected?.Invoke(detectedEnemy);
        }
    }

    /// <summary>
    /// 가장 가까운 적 객체를 반환합니다.
    /// </summary>
    /// <param name="detectedEnemies">감지된 적 컬라이더 들을 전달합니다.</param>
    /// <returns>가장 가까운 적 객체를 반환합니다.</returns>
    public EnemyCharacter GetNearestEnemy(Collider[] detectedEnemies)
    {
        // 적 객체가 여러개 감지된 경우 가까운 순서대로 정렬합니다.(오름차순 정렬)
        if (detectedEnemies.Length > 1)
        {
            // 가장 가까운 위치의 적을 찾습니다.
            for (int i = 0; i < detectedEnemies.Length - 1; ++i)
            {
                for (int j = 1; j < detectedEnemies.Length; ++j)
                {
                    Collider enemy1 = detectedEnemies[i];
                    Collider enemy2 = detectedEnemies[j];

                    // 감지 중심 위치를 얻습니다.
                    Vector3 centerPosition = transform.position + m_AreaOffset;

                    // 감지된 적들 중 가장 가까운 적을 찾기 위해 거리를 계산합니다.
                    float enemy1Distance = Vector3.Distance(centerPosition, enemy1.transform.position);
                    float enemy2Distance = Vector3.Distance(centerPosition, enemy2.transform.position);

                    // 적1의 거리가 적2보다 멀다면
                    if (enemy1Distance > enemy2Distance)
                    {
                        // 서로 위치를 교환합니다.
                        detectedEnemies[i] = enemy2;
                        detectedEnemies[j] = enemy1;
                    }
                }

            }
        }
        // 가장 가까운 적 객체를 반환합니다.
        return detectedEnemies[0].GetComponent<EnemyCharacter>();
    }

    /// <summary>
    /// 적 감지를 시작합니다.
    /// </summary>
    public void StartDetectEnemy()
    {
        _AllowDetectEnemy = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position + m_AreaOffset,
            m_DetectRadius);
    }

#endif

}
