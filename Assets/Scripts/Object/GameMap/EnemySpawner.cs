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
        // 적 캐릭터를 생성합니다.
        EnemyCharacter newEnemy = Instantiate(m_SampleEnemyCharacter);

        // 적 생성 위치를 얻습니다.
        Vector3 spawnPosition = GameMapBlock.EnemySpawnBlock.transform.position;

        // 적 생성 위치 설정
        newEnemy.transform.position = spawnPosition;
    }
}
