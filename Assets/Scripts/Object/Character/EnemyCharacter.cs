using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    protected override void Awake()
    {
        base.Awake();

        // �� ĳ������ ���̾ �����մϴ�.
        gameObject.layer = LayerMask.NameToLayer(Constants.ENEMY_LAYER_NAME);
    }

}
