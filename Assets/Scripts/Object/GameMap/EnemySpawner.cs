using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // TODO Sample
    public EnemyCharacter m_SampleEnemyCharacter;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // �� ĳ���͸� �����մϴ�.
        EnemyCharacter newEnemy = Instantiate(m_SampleEnemyCharacter);

        // �� ���� ��ġ�� ����ϴ�.
        Vector3 spawnPosition = GameMapBlock.EnemySpawnBlock.transform.position;

        // �� ���� ��ġ ����
        newEnemy.transform.position = spawnPosition;
    }
}
