using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// ���콺�� ����Ű�� ���� ��ġ�� ��Ÿ���ϴ�
    /// </summary>
    public Vector3 m_CursorWorldPosition;

    private void Update()
    {
        Debug.Log(Input.mousePosition);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 cameraPosition = Camera.main.transform.position;  
    }
#endif
}
