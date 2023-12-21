using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// GameScene UI�� ��Ÿ���ϴ�.
    /// </summary>
    [Header("UI")]
    public GameSceneUI m_GameSceneUI;

    /// <summary>
    /// �ͷ� �������� ��Ÿ���� Scriptable Object ������ ��Ÿ���ϴ�.
    /// </summary>
    [Header("ScriptableObject")]
    public TurretInfo m_TurretInfoScriptableObject;

    /// <summary>
    /// Ŀ���� ���õ� ���� �� ��� ��ü�� ��Ÿ���ϴ�.
    /// </summary>
    private GameMapBlock _SelectedGameMapBlock;

    /// <summary>
    /// �÷��̾� ���¸� ��Ÿ���ϴ�.
    /// </summary>
    private PlayerState _PlayerState;

    /// <summary>
    /// �ͷ� ��ġ������ ��Ÿ���ϴ�,
    /// </summary>
    private bool _IsBulidingTurret;

    /// <summary>
    /// ���콺�� ����Ű�� ���� ��ġ�� ��Ÿ���ϴ�.
    /// </summary>
    private Vector3 _CursorWorldPosition;

    /// <summary>
    /// ���콺 Ŀ���� �� ���� ��ġ�ϴ����� ��Ÿ���ϴ�.
    /// </summary>
    private bool _CursorInMap;

    /// <summary>
    /// �ͷ� ��ġ �� ��ġ�ǰ� �ִ� �ͷ� ��ü�� ��Ÿ���ϴ�.
    /// </summary>
    private TurretCharacter _PreviewTurret;




    private void Awake()
    {
        m_GameSceneUI.createTurret1ButtonEvent += () => CreateTurret(TurretType.Turret1);
    }

    private void Update()
    {
        // ����ĳ��Ʈ ����
        DoRaycastToCursorPosition();

        // �Է� �̺�Ʈ ó��
        InputEvent();

        if (_PlayerState == PlayerState.TurretBuildMode)
        {
            // �̸������ �ͷ� ����
            UpdatePreviewTurret();
        }
    }

    /// <summary>
    /// ����� �Է� ó���� �����մϴ�.
    /// </summary>
    private void InputEvent()
    {
        // ���� Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            switch (_PlayerState)
            {
                case PlayerState.DefaultMode:
                    break;

                case PlayerState.TurretBuildMode:
                    // ���õ� ���� �� ����� �������� �ʴ´ٸ� break;
                    if (_SelectedGameMapBlock == null) break;

                    // �ͷ� ��ġ �Ұ��� ������ ����̶�� ��ġ�� �ߴ��մϴ�.
                    if (_SelectedGameMapBlock.isTurretExist) break;

                    // �� ��Ͽ� �ͷ��� �����մϴ�.
                    _SelectedGameMapBlock.SetTurret(_PreviewTurret);

                    // �÷��̾� ���¸� �⺻ ���� �����մϴ�.
                    _PlayerState = PlayerState.DefaultMode;

                    // �̸����� ���� �ͷ��� �̸����� ���¸� �����ϴ�.
                    _PreviewTurret.FinishPreviewMode();

                    // �̸����� �ͷ��� ���ϴ�.
                    _PreviewTurret = null;
                    break;
            }
        }
    }

    /// <summary>
    /// Ŀ�� ��ġ�� ����ĳ��Ʈ�� �����մϴ�.
    /// </summary>
    private void DoRaycastToCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;

        bool objectExist = Physics.Raycast(ray, out hitResult);

        // ������ ������Ʈ�� �����ϴ� ���
        if (objectExist)
        {
            // Ŀ�� ��ġ�� �����մϴ�.
            _CursorInMap = true;
            _CursorWorldPosition = hitResult.point;

            // ������ �� ��� ������Ʈ�� ����ϴ�.
            GameMapBlock mapBlock = hitResult.transform.gameObject.
                GetComponent<GameMapBlock>();

            // ���� �� ��� ����
            SelectGameMapBlock(mapBlock);

        }
        // ������ ������Ʈ�� �������� �ʴ� ���
        else
        {
            _CursorInMap = false;

            // ���� �� ��� ���� ����
            UnselectGameMapBlock();
        }
    }

    /// <summary>
    /// �̸������ �ͷ��� �����մϴ�.
    /// </summary>
    private void UpdatePreviewTurret()
    {
        // ���콺�� ����Ű�� �ִ� �� ����� �����ϴ��� �˻��մϴ�.
        if (_SelectedGameMapBlock)
        {
            // �� ��� ��ġ�� ����ϴ�.
            Vector3 mapBlockPosition = _SelectedGameMapBlock.transform.position;

            // �̸����� �ͷ� ��ġ�� �����մϴ�.
            _PreviewTurret.transform.position = mapBlockPosition;

            // ������Ʈ Ȱ��ȭ
            _PreviewTurret.gameObject.SetActive(true);

            // �ͷ� ��ġ ���¸� ǥ���մϴ�.
            _PreviewTurret.SetPreviewState(!_SelectedGameMapBlock.isTurretExist);

        }
        else
        {
            // ������Ʈ ��Ȱ��ȭ
            _PreviewTurret.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// ���� �� ����� �����մϴ�.
    /// </summary>
    /// <param name="mapBlock">���ý�ų ���� �� ��� ��ü�� �����մϴ�.</param>
    private void SelectGameMapBlock(GameMapBlock mapBlock)
    {
        // ���ο� ����� ���õǴ� ���
        if (_SelectedGameMapBlock != mapBlock)
        {
            // ������ ���õ� ����� �����ߴٸ�
            if (_SelectedGameMapBlock != null)

                // ���� ��� ���� ����
                _SelectedGameMapBlock.OnMapBlockUnselected();

            // ���ο� ��� ��ü ����
            _SelectedGameMapBlock = mapBlock;

            // ��� ���õ� �޼��� ȣ��
            _SelectedGameMapBlock.OnMapBlockSelected();
        }
    }

    /// <summary>
    /// ���� �� ����� ���� �����մϴ�.
    /// </summary>
    private void UnselectGameMapBlock()
    {
        _SelectedGameMapBlock?.OnMapBlockUnselected();
        _SelectedGameMapBlock = null;
    }

    /// <summary>
    /// �ͷ��� �����ϴ� �޼����Դϴ�.
    /// GameSceneUI �� �ͷ� ���� ��ư Ŭ�� �̺�Ʈ�� ���˴ϴ�.
    /// </summary>
    /// <param name="turretType">������ų �ͷ� Ÿ���� �����մϴ�.</param>
    private void CreateTurret(TurretType turretType)
    {
        // �⺻ ���°� �ƴ϶�� �޼��� ȣ�� ����
        if (_PlayerState != PlayerState.DefaultMode) return;

        // �ͷ� ���� ���� �����մϴ�.
        _PlayerState = PlayerState.TurretBuildMode;

        // �̸����� �ͷ� ����
        CreatePreviewTurret(turretType);
    }

    /// <summary>
    /// �̸������ �ͷ��� �����մϴ�.
    /// </summary>
    /// <param name="turretType">������ �ͷ� Ÿ���� �����մϴ�.</param>
    private void CreatePreviewTurret(TurretType turretType)
    {
        // �ͷ� ��ġ ���·� �����մϴ�.
        _IsBulidingTurret = true;

        // �̸����� �ͷ� ������Ʈ ����
        TurretData? turretData = m_TurretInfoScriptableObject.GetTurretData(turretType);

        //if (turretData == null)
        if (!turretData.HasValue)
        {
            Debug.LogError($"���������� ���� �ͷ� Ÿ���� ���޵Ǿ����ϴ�. ({turretType})");
            return;
        }

        // �ͷ� ������Ʈ�� �����մϴ�.
        TurretCharacter previewTurret = Instantiate(turretData.Value.turretPrefab);

        // �̸������ ���͸����� ����ϴ�.
        Material previewMaterial = m_TurretInfoScriptableObject.m_TurretPreviewMaterial;

        // �̸������ �ͷ� ��ü �ʱ�ȭ
        previewTurret.InitializeTurretCharacter(previewMaterial, turretData.Value);

        // �̸����� ���� �����մϴ�.
        previewTurret.SetPreviewMode();

        // �̸������ �ͷ����� �����մϴ�.
        _PreviewTurret = previewTurret;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // ���õ� ����� �����ϴ� ���
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
