
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
}