using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button selectLevelButton;
    [SerializeField] private Button showControlsButton;
    [SerializeField] private Button quitButton;

    [Space]
    [Header("Pop-ups")]
    [SerializeField] private Transform levelSelectionPopUpTransform;
    [SerializeField] private Transform controlsPopUpTransform;

    private ControlsPopUp controlsPopUp;
    private LevelSelectionPopUp levelSelectionPopUp;

    private void OnEnable()
    {
        controlsPopUp = controlsPopUpTransform
            .GetComponent<ControlsPopUp>();

        levelSelectionPopUp = levelSelectionPopUpTransform
            .GetComponent<LevelSelectionPopUp>();

        AddButtonsListeners();
    }

    private void OnDisable()
    {
        RemoveButtonsListeners();
    }

    private void LoadFirstLevel()
    {
        LevelManager.LoadFirstLevel();
    }

    private void QuitGame()
    {
#if UNITY_WEBGL
        var homePageUrl = "https://ivaylolivanov.github.io/";
        Application.ExternalEval("window.open('" + homePageUrl + "','_self')");
#endif
        Application.Quit();
    }

    private void AddButtonsListeners()
    {
        playButton.onClick.AddListener(LoadFirstLevel);
        selectLevelButton.onClick.AddListener(levelSelectionPopUp.Open);
        showControlsButton.onClick.AddListener(controlsPopUp.Open);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void RemoveButtonsListeners()
    {
        playButton.onClick.RemoveListener(LoadFirstLevel);
        selectLevelButton.onClick.RemoveListener(levelSelectionPopUp.Open);
        showControlsButton.onClick.RemoveListener(controlsPopUp.Open);
        quitButton.onClick.RemoveListener(QuitGame);
    }
}
