

/// <summary>
/// 플레이어 상태를 나타내기 위한 열거 형식입니다.
/// </summary>
public enum PlayerState : sbyte
{
    // 기본 상태
    DefaultMode         = 0b0000,

    // 터렛 생성 모드
    TurretBuildMode     = 0b0010,
}