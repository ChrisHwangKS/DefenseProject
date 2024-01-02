using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̱���
// Ŭ������ �ν��Ͻ��� �� �ϳ��� �����ǵ��� �ϴ� ������ ����

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �������� ��Ÿ���ϴ�.
    /// </summary>
    [HideInInspector]
    public List<GameMapBlock> m_Stopovers;

    /// <summary>
    /// GameManager ��ü�� ��Ÿ���ϴ�.
    /// </summary>
    private static GameManager _Instance;

    public static GameManager instance => GetGameManager();

    private void Awake()
    {
        // GameManager ��ü�� �̹� ������ ���
        if (_Instance != null && _Instance != this)
        {
            // �� ������Ʈ�� �����մϴ�.
            Destroy(gameObject);
            return;
        }

        // ���� ����Ǿ �ش� ������Ʈ�� ���� ������Ʈ�� ���ŵ��� �ʵ��� �մϴ�.
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// GameManager ��ü�� ��ȯ�մϴ�.
    /// </summary>
    /// <returns></returns>
    public static GameManager GetGameManager()
    {
        // ������ ��ü�� �������� �ʴ� ���
        if(!_Instance)
        {
            _Instance = FindObjectOfType<GameManager>();
            /// FindObjectOfType<T>() : T ������ ��ü�� ���忡�� ã�� ��ȯ�մϴ�.
        }
        return _Instance;

    }
}
