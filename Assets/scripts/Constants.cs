using System.Collections.Generic;

public class Constants
{
    // Dvd constants
    public const int BASE_DVD_SPEED = 5;
    public const int BASE_DVD_COUNT = 1;
    public const float IGNORE_WALL_COLLISION_COOLDOWN = 0.5f;

    // Points constants
    public const int BASE_POINTS = 0;
    public const int BASE_POINT_GAIN = 1;

    // Combo constants
    public const int PERFECT_STREAK_NEEDED = 2;
    public const float BASE_PERFECT_STREAK_MULTIPLIER = 1;
    public const float BASE_TIMED_COMBO_MULTIPLER = 1f;
    public const float BASE_TIMED_COMBO_DURATION = 10f;

    // Upgrades constants
    public const float SPEED_INCREASE = 0.1f;
    public const float POINTS_INCREASE = 0.25f;
    public const float CORNER_SCALE_INCREASE = 0.05f;
    public const float BASE_CORNER_SCALE = 1f;
    public const float PERFECT_STREAK_MULTIPLIER_INCREASE = 0.5f;
    public const float TIMED_COMBO_MULTIPLIER_INCREASE = 0.25f;
    public const float TIMED_COMBO_DURATION_INCREASE = 0.25f;
    public const int CRITICAL_HIT_INCREASE = 2;
    public const int BASE_CRITICAL_HIT_CHANCE = 0;

    // Rebirth upgrades constants
    public const int REBIRTH_COST = 50000;
    public const int REBIRTH_SPEED_INCREASE_COUNT = 0;
    public const int REBIRTH_DVD_COUNT = 1;
    public const int REBIRTH_POINTS_INCREASE_COUNT = 0;
    public const float REBIRTH_CORNER_SCALE_INCREASE = 0.5f;

    // Upgrades base cost
    public const int SPEED_BASE_COST = 5;
    public const int POINTS_BASE_COST = 2;
    public const int DVD_BASE_COST = 20;
    public const int CORNER_SCALE_BASE_COST = 5;
    public const int PERFECT_STREAK_MULTIPLIER_COST = 10;
    public const int TIMED_COMBO_MULTIPLIER_BASE_COST = 15;
    public const int TIMED_COMBO_DURATION_BASE_COST = 20;
    public const int CRITICAL_HIT_BASE_COST = 50;

    // Upgrades names
    public const string SPEED_INCREASE_NAME = "Speed Boost";
    public const string POINTS_INCREASE_NAME = "Point Surge";
    public const string DVD_COUNT_NAME = "Multidisc";
    public const string CORNER_SCALE_NAME = "Corner Mastery";
    public const string PERFECT_STREAK_MULTIPLIER_NAME = "Perfect Streak";
    public const string TIMED_COMBO_MULTIPLIER_NAME = "Timed Combo Amplifier";
    public const string TIMED_COMBO_DURATION_NAME = "Timed Combo Extension";
    public const string CRITICAL_HIT_NAME = "Critical Hit";
    public const string REBIRTH_SPEED_INCREASE_NAME = "RebirthSpeedIncrease";
    public const string REBIRTH_DVD_COUNT_NAME = "RebirthDvdCount";
    public const string REBIRTH_POINTS_INCREASE_NAME = "RebirthPointIncrease";

    public static Dictionary<string, string> UPGRADES_DESCRIPTIONS = new Dictionary<string, string>
    {
        [SPEED_INCREASE_NAME] = "Increases the speed of the dvd by 10%",
        [POINTS_INCREASE_NAME] = "Increases the amount of points gained per hit by 25%",
        [DVD_COUNT_NAME] = "Spawns one more dvd",
        [CORNER_SCALE_NAME] = "Expands corner hitboxes by 5%",
        [PERFECT_STREAK_MULTIPLIER_NAME] = "Consecutive hits without a wall increase points by 50% per level",
        [TIMED_COMBO_MULTIPLIER_NAME] = "Boosts the timed combo multiplier",
        [TIMED_COMBO_DURATION_NAME] = "Extends the timed combo duration",
        [CRITICAL_HIT_NAME] = "2% higher chance for critical hits (3x points)"
    };

}
