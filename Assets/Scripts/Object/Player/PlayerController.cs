using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// GameScene UI를 나타냅니다.
    /// </summary>
    [Header("UI")]
    public GameSceneUI m_GameSceneUI;

    /// <summary>
    /// 터렛 정보들을 나타내는 Scriptable Object 에셋을 나타냅니다.
    /// </summary>
    [Header("ScriptableObject")]
    public TurretInfo m_TurretInfoScriptableObject;

    /// <summary>
    /// 커서로 선택된 게임 맵 블록 객체를 나타냅니다.
    /// </summary>
    private GameMapBlock _SelectedGameMapBlock;

    /// <summary>
    /// 플레이어 상태를 나타냅니다.
    /// </summary>
    private PlayerState _PlayerState;

    /// <summary>
    /// 터렛 설치중임을 나타냅니다,
    /// </summary>
    private bool _IsBulidingTurret;

    /// <summary>
    /// 마우스가 가리키는 월드 위치를 나타냅니다.
    /// </summary>
    private Vector3 _CursorWorldPosition;

    /// <summary>
    /// 마우스 커서가 맵 내에 위치하는지를 나타냅니다.
    /// </summary>
    private bool _CursorInMap;

    /// <summary>
    /// 터렛 배치 시 배치되고 있는 터렛 객체를 나타냅니다.
    /// </summary>
    private TurretCharacter _PreviewTurret;




    private void Awake()
    {
        m_GameSceneUI.createTurret1ButtonEvent += () => CreateTurret(TurretType.Turret1);
    }

    private void Update()
    {
        // 레이캐스트 진행
        DoRaycastToCursorPosition();

        // 입력 이벤트 처리
        InputEvent();

        if (_PlayerState == PlayerState.TurretBuildMode)
        {
            // 미리보기용 터렛 갱신
            UpdatePreviewTurret();
        }
    }

    /// <summary>
    /// 사용자 입력 처리를 진행합니다.
    /// </summary>
    private void InputEvent()
    {
        // 왼쪽 클릭
        if (Input.GetMouseButtonDown(0))
        {
            switch (_PlayerState)
            {
                case PlayerState.DefaultMode:
                    break;

                case PlayerState.TurretBuildMode:
                    // 선택된 게임 맵 블록이 존재하지 않는다면 break;
                    if (_SelectedGameMapBlock == null) break;

                    // 터렛 설치 불가능 상태의 블록이라면 설치를 중단합니다.
                    if (_SelectedGameMapBlock.isTurretExist) break;

                    // 맵 블록에 터렛을 설정합니다.
                    _SelectedGameMapBlock.SetTurret(_PreviewTurret);

                    // 플레이어 상태를 기본 모드로 변경합니다.
                    _PlayerState = PlayerState.DefaultMode;

                    // 미리보기 상태 터렛의 미리보기 상태를 끝냅니다.
                    _PreviewTurret.FinishPreviewMode();

                    // 미리보기 터렛을 비웁니다.
                    _PreviewTurret = null;
                    break;
            }
        }
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
        if (objectExist)
        {
            // 커서 위치를 설정합니다.
            _CursorInMap = true;
            _CursorWorldPosition = hitResult.point;

            // 감지된 맵 블록 컴포넌트를 얻습니다.
            GameMapBlock mapBlock = hitResult.transform.gameObject.
                GetComponent<GameMapBlock>();

            // 게임 맵 블록 선택
            SelectGameMapBlock(mapBlock);

        }
        // 감지된 오브젝트가 존재하지 않는 경우
        else
        {
            _CursorInMap = false;

            // 게임 맵 블록 선택 해제
            UnselectGameMapBlock();
        }
    }

    /// <summary>
    /// 미리보기용 터렛을 갱신합니다.
    /// </summary>
    private void UpdatePreviewTurret()
    {
        // 마우스로 가리키고 있는 맵 블록이 존재하는지 검사합니다.
        if (_SelectedGameMapBlock)
        {
            // 맵 블록 위치를 얻습니다.
            Vector3 mapBlockPosition = _SelectedGameMapBlock.transform.position;

            // 미리보기 터렛 위치를 설정합니다.
            _PreviewTurret.transform.position = mapBlockPosition;

            // 오브젝트 활성화
            _PreviewTurret.gameObject.SetActive(true);

            // 터렛 배치 상태를 표시합니다.
            _PreviewTurret.SetPreviewState(!_SelectedGameMapBlock.isTurretExist);

        }
        else
        {
            // 오브젝트 비활성화
            _PreviewTurret.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// 게임 맵 블록을 선택합니다.
    /// </summary>
    /// <param name="mapBlock">선택시킬 게임 맵 블록 객체를 전달합니다.</param>
    private void SelectGameMapBlock(GameMapBlock mapBlock)
    {
        // 새로운 블록이 선택되는 경우
        if (_SelectedGameMapBlock != mapBlock)
        {
            // 기존에 선택된 블록이 존재했다면
            if (_SelectedGameMapBlock != null)

                // 이전 블록 선택 해제
                _SelectedGameMapBlock.OnMapBlockUnselected();

            // 새로운 블록 객체 설정
            _SelectedGameMapBlock = mapBlock;

            // 블록 선택됨 메서드 호출
            _SelectedGameMapBlock.OnMapBlockSelected();
        }
    }

    /// <summary>
    /// 게임 맵 블록을 선택 해제합니다.
    /// </summary>
    private void UnselectGameMapBlock()
    {
        _SelectedGameMapBlock?.OnMapBlockUnselected();
        _SelectedGameMapBlock = null;
    }

    /// <summary>
    /// 터렛을 생성하는 메서드입니다.
    /// GameSceneUI 의 터렛 생성 버튼 클릭 이벤트로 사용됩니다.
    /// </summary>
    /// <param name="turretType">생성시킬 터렛 타입을 전달합니다.</param>
    private void CreateTurret(TurretType turretType)
    {
        // 기본 상태가 아니라면 메서드 호출 종료
        if (_PlayerState != PlayerState.DefaultMode) return;

        // 터렛 생성 모드로 설정합니다.
        _PlayerState = PlayerState.TurretBuildMode;

        // 미리보기 터렛 생성
        CreatePreviewTurret(turretType);
    }

    /// <summary>
    /// 미리보기용 터렛을 생성합니다.
    /// </summary>
    /// <param name="turretType">생성할 터렛 타입을 전달합니다.</param>
    private void CreatePreviewTurret(TurretType turretType)
    {
        // 터렛 설치 상태로 설정합니다.
        _IsBulidingTurret = true;

        // 미리보기 터렛 오브젝트 생성
        TurretData? turretData = m_TurretInfoScriptableObject.GetTurretData(turretType);

        //if (turretData == null)
        if (!turretData.HasValue)
        {
            Debug.LogError($"정상적이지 않은 터렛 타입이 전달되었습니다. ({turretType})");
            return;
        }

        // 터렛 오브젝트를 생성합니다.
        TurretCharacter previewTurret = Instantiate(turretData.Value.turretPrefab);

        // 미리보기용 메터리얼을 얻습니다.
        Material previewMaterial = m_TurretInfoScriptableObject.m_TurretPreviewMaterial;

        // 미리보기용 터렛 객체 초기화
        previewTurret.InitializeTurretCharacter(previewMaterial, turretData.Value);

        // 미리보기 모드로 설정합니다.
        previewTurret.SetPreviewMode();

        // 미리보기용 터렛으로 설정합니다.
        _PreviewTurret = previewTurret;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 선택된 블록이 존재하는 경우
        if (_SelectedGameMapBlock)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                Camera.main.transform.position,
                _CursorWorldPosition);

            Gizmos.DrawCube(
                _SelectedGameMapBlock.transform.position,
                new Vector3(Constants.MAP_BLOCK_SIZE, 0.2f, Constants.MAP_BLOCK_SIZE));
        }
    }
#endif




}
