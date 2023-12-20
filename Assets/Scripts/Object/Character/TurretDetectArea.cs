using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetectArea : MonoBehaviour
{
    [Header("���� �ݰ�")]
    public float m_DetectRadius;

    [Header("���� �ݰ� ������")]
    public Vector3 m_AreaOffset;

    /// <summary>
    /// �� ������ ����մϴ�.
    /// </summary>
    private bool _AllowDetectEnemy;

    /// <summary>
    /// ���� �����Ǿ��� ��� �߻���ų �̺�Ʈ�Դϴ�.
    /// Collider[] detectedEnemyColliders : ������ �� �ö��̴� ��ü���� ���޵˴ϴ�.
    /// </summary>
    public event Action<Collider[] /*detectedEnemyColliders*/> onEnemyDetected;

    private void Update()
    {
        // �� ������ ���� ��� �� ������ �����մϴ�.
        if (_AllowDetectEnemy) DoDetect();
    }

    /// <summary>
    /// ���� �����մϴ�.
    /// </summary>
    private void DoDetect()
    {
        // �� ������Ʈ ���̾� ����ũ�� ����ϴ�.
        int layerMask = 1 << LayerMask.NameToLayer(Constants.ENEMY_LAYER_NAME);

        // �ͷ� ��ġ���� m_DetectRadius �ݰ� ��ŭ�� ���� ������ �� ������Ʈ�� �����մϴ�.
        Collider[] detectedEnemy = Physics.OverlapSphere(
            transform.position + m_AreaOffset,
            m_DetectRadius,
            layerMask);

        // �ͷ����� ���� ����� ���� ���ϴ�.
        Collider nearestEnemy = null;

        // �ϳ� �̻��� ���� ������ ���
        if(detectedEnemy.Length > 0)
        {
            // �� ���� �̺�Ʈ�� �߻���ŵ�ϴ�.
            onEnemyDetected?.Invoke(detectedEnemy);
        }
    }

    /// <summary>
    /// ���� ����� �� ��ü�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="detectedEnemies">������ �� �ö��̴� ���� �����մϴ�.</param>
    /// <returns>���� ����� �� ��ü�� ��ȯ�մϴ�.</returns>
    public EnemyCharacter GetNearestEnemy(Collider[] detectedEnemies)
    {
        // �� ��ü�� ������ ������ ��� ����� ������� �����մϴ�.(�������� ����)
        if (detectedEnemies.Length > 1)
        {
            // ���� ����� ��ġ�� ���� ã���ϴ�.
            for (int i = 0; i < detectedEnemies.Length - 1; ++i)
            {
                for (int j = 1; j < detectedEnemies.Length; ++j)
                {
                    Collider enemy1 = detectedEnemies[i];
                    Collider enemy2 = detectedEnemies[j];

                    // ���� �߽� ��ġ�� ����ϴ�.
                    Vector3 centerPosition = transform.position + m_AreaOffset;

                    // ������ ���� �� ���� ����� ���� ã�� ���� �Ÿ��� ����մϴ�.
                    float enemy1Distance = Vector3.Distance(centerPosition, enemy1.transform.position);
                    float enemy2Distance = Vector3.Distance(centerPosition, enemy2.transform.position);

                    // ��1�� �Ÿ��� ��2���� �ִٸ�
                    if (enemy1Distance > enemy2Distance)
                    {
                        // ���� ��ġ�� ��ȯ�մϴ�.
                        detectedEnemies[i] = enemy2;
                        detectedEnemies[j] = enemy1;
                    }
                }

            }
        }
        // ���� ����� �� ��ü�� ��ȯ�մϴ�.
        return detectedEnemies[0].GetComponent<EnemyCharacter>();
    }

    /// <summary>
    /// �� ������ �����մϴ�.
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
