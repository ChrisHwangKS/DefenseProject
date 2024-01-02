using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤
// 클래스의 인스턴스가 단 하나만 생성되도록 하는 디자인 패턴

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 경유지를 나타냅니다.
    /// </summary>
    [HideInInspector]
    public List<GameMapBlock> m_Stopovers;

    /// <summary>
    /// GameManager 객체를 나타냅니다.
    /// </summary>
    private static GameManager _Instance;

    public static GameManager instance => GetGameManager();

    private void Awake()
    {
        // GameManager 객체가 이미 생성된 경우
        if (_Instance != null && _Instance != this)
        {
            // 이 오브젝트를 제거합니다.
            Destroy(gameObject);
            return;
        }

        // 씬이 변경되어도 해당 컴포넌트를 갖는 오브젝트는 제거되지 않도록 합니다.
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// GameManager 객체를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public static GameManager GetGameManager()
    {
        // 생생된 객체가 존재하지 않는 경우
        if(!_Instance)
        {
            _Instance = FindObjectOfType<GameManager>();
            /// FindObjectOfType<T>() : T 형식의 객체를 월드에서 찾아 반환합니다.
        }
        return _Instance;

    }
}
