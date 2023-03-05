using UnityEngine;

public static class PlayerProgressUtility
{
    private const string HIGHEST_LEVEL_COMPLETED = "HIGHEST LEVEL COMPLETED";
    private const string CONTROLS_SHOWN_ON_1ST_LEVEL = "CONTROLS SHOWN ON 1ST LEVEL";

    public static void UpdateLevelCompleted(int buildIndex)
    {
        PlayerPrefs.SetInt(HIGHEST_LEVEL_COMPLETED, buildIndex);
    }

    public static int GetHighestLevelCompleted()
    {
        int highestLevelCompleted = LevelManager.FIRST_LEVEL_BUILD_INDEX;
        if (PlayerPrefs.HasKey(HIGHEST_LEVEL_COMPLETED))
            highestLevelCompleted = PlayerPrefs.GetInt(HIGHEST_LEVEL_COMPLETED);

        return highestLevelCompleted;
    }

    public static void SetControlsShownOn1stLevel()
        => PlayerPrefs.SetInt(CONTROLS_SHOWN_ON_1ST_LEVEL, 1);

    public static bool GetControlsShownOn1stLevel()
    {
        var result = false;

        if (PlayerPrefs.HasKey(CONTROLS_SHOWN_ON_1ST_LEVEL))
            result = PlayerPrefs.GetInt(CONTROLS_SHOWN_ON_1ST_LEVEL) == 1;

        return result;
    }
}
