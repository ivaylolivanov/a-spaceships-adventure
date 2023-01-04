using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Delays")]
    [SerializeField] private float reloadLevelDelay = 3f;
    [SerializeField] private float loadNextLevelDelay = 1f;

    public const int FIRST_LEVEL_BUILD_INDEX = 1;
    private const int MENU_SCENE_BUILD_INDEX  = 0;

    private void Update()
    {
        if (InputReader.Debug_LoadNextLevelKeyDown)
            LoadNextLevel();
    }

    public static int GetCurrentSceneIndex()
        => SceneManager.GetActiveScene().buildIndex;

    public static bool IsOnLevelScene()
    {
        int currentSceneIndex = GetCurrentSceneIndex();

        bool result = currentSceneIndex >= FIRST_LEVEL_BUILD_INDEX;
        return result;
    }

    public static int GetLevelsCount()
        => SceneManager.sceneCountInBuildSettings
            - FIRST_LEVEL_BUILD_INDEX;

    public static void LoadFirstLevel()
        => SceneManager.LoadScene(FIRST_LEVEL_BUILD_INDEX);

    public static void LoadMenuScene()
        => SceneManager.LoadScene(MENU_SCENE_BUILD_INDEX);

    public static void ReloadCurrentLevel()
    {
        int currentLevel = GetCurrentSceneIndex();
        SceneManager.LoadScene(currentLevel);
    }

    public static void LoadNextLevel()
    {
        int currentLevel = GetCurrentSceneIndex();
        int nextLevelIndex = currentLevel + 1;

        LoadLevel(nextLevelIndex);
    }

    public static void LoadLevel(int levelBuildIndex)
    {
        if (!IsSceneIndexValidLevelIndex(levelBuildIndex))
            return;

        SceneManager.LoadScene(levelBuildIndex);
    }

    public static bool IsSceneIndexValid(int index)
        => index >= 0 && index < SceneManager.sceneCountInBuildSettings;

    public static bool IsSceneIndexValidLevelIndex(int index)
        => index >= FIRST_LEVEL_BUILD_INDEX
            && index < SceneManager.sceneCountInBuildSettings;

    private void DelayedReloadCurrentLevel()
        => Invoke("ReloadCurrentLevel", reloadLevelDelay);

    private void DelayedLoadNextLevel()
        => Invoke("LoadNextLevel", loadNextLevelDelay);
}
