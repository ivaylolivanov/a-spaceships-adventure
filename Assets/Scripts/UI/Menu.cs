using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button selectLevelButton;
    [SerializeField] private Button quitButton;

    [Header("Level selection pop up")]
    [SerializeField] private Transform levelSelectionPopUp;

    private void OnEnable()
    {
        InitializeButtons();
        HideLevelSelectionPopUp();
    }

    private void OnDisable()
    {
        UninitializeButtons();
        HideLevelSelectionPopUp();
    }

    private void LoadFirstLevel()
    {
        LevelManager.LoadFirstLevel();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void InitializeButtons()
    {
        playButton.onClick.AddListener(LoadFirstLevel);
        selectLevelButton.onClick.AddListener(ShowLevelSelectionPopUp);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void UninitializeButtons()
    {
        playButton.onClick.RemoveListener(LoadFirstLevel);
        selectLevelButton.onClick.RemoveListener(ShowLevelSelectionPopUp);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void ShowLevelSelectionPopUp()
    {
        levelSelectionPopUp.localScale = Vector3.one;
    }

    private void HideLevelSelectionPopUp()
    {
        levelSelectionPopUp.localScale = Vector3.zero;
    }
}
