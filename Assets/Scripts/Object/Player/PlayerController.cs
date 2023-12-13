using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// 커서로 선택된 게임 맵 블록 객체를 나타냅니다.
    /// </summary>
    private GameMapBlock _SelectedGameMapBlock;

    /// <summary>
    /// 터렛 설치중임을 나타냅니다.
    /// </summary>
    private bool _IsBuildingTurret;

    /// <summary>
    /// 마우스가 가리키는 월드 위치를 나타냅니다
    /// </summary>
    public Vector3 m_CursorWorldPosition;



    private void Update()
    {
        // 레이캐스트 진행
        DoRaycastToCursorPosition();
    }

    /// <summary>
    /// 커서 위치로 레이캐스트를 진행합니다.
    /// </summary>
    private void DoRaycastToCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;

        bool objectExist = Physics.Raycast(ray, out hitResult);

        // 감지된 오브젝트가 존재하는 경우
        if(objectExist)
        {
            // 감지된 맵 블록 컴포넌트를 얻습니다.
            GameMapBlock mapBlock = hitResult.transform.gameObject.GetComponent<GameMapBlock>();

            // 게임 맵 블록 선택
            SelectGameMapBlock(mapBlock);
        }
        // 감지된 오브젝트가 존재하지 않는 경우
        else
        {
            // 게임 맵 블록 선택 해제
            UnselectGameMapBlock();
        }
    }

    /// <summary>
    /// 게임 맵 블록을 선택합니다.
    /// </summary>
    /// <param name="mapBlock">선택시킬 게임 맵 블록 객체를 전달합니다.</param>
    private void SelectGameMapBlock(GameMapBlock mapBlock)
    {
        // 새로운 블록이 선택되는 경우
        if(_SelectedGameMapBlock != mapBlock)
        {
            // 기존에 선택된 블록이 존재했다면
            if(_SelectedGameMapBlock != null)
            {
                // 이전 블록 선택 해제
                _SelectedGameMapBlock.OnMapBlockUnselected();
            }

            // 새로운 블록 객체 설정
            _SelectedGameMapBlock = mapBlock;

            // 블록 선택됨 메서드 호출
            _SelectedGameMapBlock.OnMapBlockSelected();
        }
    }

    /// <summary>
    /// 게임 맵 블록을 선택 해제 합니다.
    /// </summary>
    private void UnselectGameMapBlock()
    {
        _SelectedGameMapBlock?.OnMapBlockUnselected();
        _SelectedGameMapBlock = null;
    }

    /// <summary>
    /// 미리보기용 터렛을 생성합니다.
    /// </summary>
    /// <param name="turretType">생성할 터렛 타입을 전달합니다.</param>
    public void CreatePreviewTurret(TurretType turretType)
    {
        // 터렛 설치 상태로 설정합니다.
        _IsBuildingTurret = true;

        // 미리보기용 터렛 오브젝트 생성
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 선택된 블록이 존재하는 경우
        if(_SelectedGameMapBlock)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                Camera.main.transform.position,
                _SelectedGameMapBlock.transform.position
                );

            Gizmos.DrawCube(
                _SelectedGameMapBlock.transform.position,
                new Vector3(Constants.MAP_BLOCK_SIZE, 0.2f, Constants.MAP_BLOCK_SIZE)
                ); ;

        }
        
    }
#endif
}
