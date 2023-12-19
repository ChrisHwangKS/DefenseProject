
using UnityEngine;
using UnityEngine.Android;
/// <summary>
/// ���ӿ� ���Ǵ� ����� �����մϴ�.
/// </summary>
public static class Constants
{
    /// <summary>
    /// �� ��� ũ�⸦ ��Ÿ���ϴ�.
    /// </summary>
    public const float MAP_BLOCK_SIZE = 0.5f;

    /// <summary>
    /// ī�޶��� ���� ���Դϴ�.
    /// </summary>
    public const float CAMERA_HEIGHT_TERM = 1.0f;

    /// <summary>
    /// �� ��� Emission �Ķ���� �̸�
    /// </summary>
    public const string MAP_BLOCK_MATPARAM_EMISSION = "_Emission";

    /// <summary>
    /// �� ����� ���õǾ��� �� _Emission �� ������ ��ġ
    /// </summary>
    public const float MAP_BLOCK_SELECTED_EMISSION = 20.0f;

    /// <summary>
    /// �� ����� ���� �����Ǿ��� �� _Emission �� ������ ��ġ
    /// </summary>
    public const float MAP_BLOCK_UNSELECTED_EMISSION = 10.0f;

    /// <summary>
    /// �ͷ� ��ġ ���� ������ ��Ÿ���ϴ�
    /// </summary>
    public static readonly Color32 TURRET_INSTALL_POSSIBLE_COLOR = new Color32(71,255,212,0);
    // readonly : �б� �����ʵ��Դϴ�.
    // �� ���� Ŭ���� �����ڿ����� ������ �����մϴ�.
    
    /// <summary>
    /// �ͷ� ��ġ �Ұ��� ������ ��Ÿ���ϴ�.
    /// </summary>
    public static readonly Color32 TURRET_INSTALL_IMPOSSIBLE_COLOR = new Color32(255,11,38,0);

    /// <summary>
    /// �� ������Ʈ�� ���̾ ��Ÿ���ϴ�.
    /// </summary>
    public const string ENEMY_LAYER_NAME = "Enemy";

}