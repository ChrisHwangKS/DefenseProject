using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 마우스가 가리키는 월드 위치를 나타냅니다
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
