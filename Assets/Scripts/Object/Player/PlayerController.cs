using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// Ŀ���� ���õ� ���� �� ��� ��ü�� ��Ÿ���ϴ�.
    /// </summary>
    private GameMapBlock _SelectedGameMapBlock;

    /// <summary>
    /// �ͷ� ��ġ������ ��Ÿ���ϴ�.
    /// </summary>
    private bool _IsBuildingTurret;

    /// <summary>
    /// ���콺�� ����Ű�� ���� ��ġ�� ��Ÿ���ϴ�
    /// </summary>
    public Vector3 m_CursorWorldPosition;



    private void Update()
    {
        // ����ĳ��Ʈ ����
        DoRaycastToCursorPosition();
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
        if(objectExist)
        {
            // ������ �� ��� ������Ʈ�� ����ϴ�.
            GameMapBlock mapBlock = hitResult.transform.gameObject.GetComponent<GameMapBlock>();

            // ���� �� ��� ����
            SelectGameMapBlock(mapBlock);
        }
        // ������ ������Ʈ�� �������� �ʴ� ���
        else
        {
            // ���� �� ��� ���� ����
            UnselectGameMapBlock();
        }
    }

    /// <summary>
    /// ���� �� ����� �����մϴ�.
    /// </summary>
    /// <param name="mapBlock">���ý�ų ���� �� ��� ��ü�� �����մϴ�.</param>
    private void SelectGameMapBlock(GameMapBlock mapBlock)
    {
        // ���ο� ����� ���õǴ� ���
        if(_SelectedGameMapBlock != mapBlock)
        {
            // ������ ���õ� ����� �����ߴٸ�
            if(_SelectedGameMapBlock != null)
            {
                // ���� ��� ���� ����
                _SelectedGameMapBlock.OnMapBlockUnselected();
            }

            // ���ο� ��� ��ü ����
            _SelectedGameMapBlock = mapBlock;

            // ��� ���õ� �޼��� ȣ��
            _SelectedGameMapBlock.OnMapBlockSelected();
        }
    }

    /// <summary>
    /// ���� �� ����� ���� ���� �մϴ�.
    /// </summary>
    private void UnselectGameMapBlock()
    {
        _SelectedGameMapBlock?.OnMapBlockUnselected();
        _SelectedGameMapBlock = null;
    }

    /// <summary>
    /// �̸������ �ͷ��� �����մϴ�.
    /// </summary>
    /// <param name="turretType">������ �ͷ� Ÿ���� �����մϴ�.</param>
    public void CreatePreviewTurret(TurretType turretType)
    {
        // �ͷ� ��ġ ���·� �����մϴ�.
        _IsBuildingTurret = true;

        // �̸������ �ͷ� ������Ʈ ����
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // ���õ� ����� �����ϴ� ���
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
