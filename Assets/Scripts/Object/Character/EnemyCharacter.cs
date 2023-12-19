using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    protected override void Awake()
    {
        base.Awake();

        // 적 캐릭터의 레이어를 지정합니다.
        gameObject.layer = LayerMask.NameToLayer(Constants.ENEMY_LAYER_NAME);
    }

}
