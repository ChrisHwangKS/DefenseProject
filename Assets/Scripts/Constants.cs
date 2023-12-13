
using UnityEngine.Android;
/// <summary>
/// 게임에 사용되는 상수를 정의합니다.
/// </summary>
public static class Constants
{
    /// <summary>
    /// 맵 블록 크기를 나타냅니다.
    /// </summary>
    public const float MAP_BLOCK_SIZE = 0.5f;

    /// <summary>
    /// 카메라의 높이 텀입니다.
    /// </summary>
    public const float CAMERA_HEIGHT_TERM = 1.0f;

    /// <summary>
    /// 맵 블록 Emission 파라미터 이름
    /// </summary>
    public const string MAP_BLOCK_MATPARAM_EMISSION = "_Emission";

    /// <summary>
    /// 맵 블록이 선택되었을 때 _Emission 에 설정될 수치
    /// </summary>
    public const float MAP_BLOCK_SELECTED_EMISSION = 20.0f;

    /// <summary>
    /// 맵 블록이 선택 해제되었을 때 _Emission 에 설정될 수치
    /// </summary>
    public const float MAP_BLOCK_UNSELECTED_EMISSION = 10.0f;
}