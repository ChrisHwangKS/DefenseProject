
using UnityEngine;
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

    /// <summary>
    /// 터렛 설치 가능 색상을 나타냅니다
    /// </summary>
    public static readonly Color32 TURRET_INSTALL_POSSIBLE_COLOR = new Color32(71,255,212,0);
    // readonly : 읽기 전용필드입니다.
    // 이 값은 클래스 생성자에서만 설정이 가능합니다.
    
    /// <summary>
    /// 터렛 설치 불가능 색상을 나타냅니다.
    /// </summary>
    public static readonly Color32 TURRET_INSTALL_IMPOSSIBLE_COLOR = new Color32(255,11,38,0);

    /// <summary>
    /// 적 오브젝트의 레이어를 나타냅니다.
    /// </summary>
    public const string ENEMY_LAYER_NAME = "Enemy";

    /// <summary>
    /// 총알 최대 이동 거리
    /// </summary>
    public const float BULLET_MAX_DISTANCE = 50.0f;

    /// <summary>
    /// 총알 속력입니다.
    /// </summary>
    public const float BULLET_SPEED = 7.0f;

    /// <summary>
    /// 총알 반지름입니다.
    /// </summary>
    public const float BULLET_RADIUS = 0.04f;

}