using UnityEngine;

public static class PlayerProgressUtility
{
    private const string HIGHEST_LEVEL_COMPLETED = "HIGHEST_LEVEL_COMPLETED";

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
}
