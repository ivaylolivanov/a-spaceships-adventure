using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStatusPopUp : PopUp
{
    [SerializeField] private Button bottomLeftButton;
    [SerializeField] private Button bottomRightButton;

    [SerializeField] private TextMeshProUGUI popUpText;

    private TextMeshProUGUI bottomLeftButtonText;
    private TextMeshProUGUI bottomRightButtonText;

    protected override void OnEnable()
    {
        base.OnEnable();

        AddCollisionListeners();
        CacheButtonsTexts();
        SetupInProgressContent();

        toggleable = true;
    }

    private void OnDisable()
    {
        RemoveButtonsListeners();
        RemoveCollisionListeners();
    }

    private void AddCollisionListeners()
    {
        CollisionHandler.OnLandingPadEnter += OpenWithCompletedContent;
        CollisionHandler.OnRocketCrash += OpenWithLostContent;
    }

    private void CacheButtonsTexts()
    {
        bottomLeftButtonText = bottomLeftButton
            .GetComponentInChildren<TextMeshProUGUI>();

        bottomRightButtonText = bottomRightButton
            .GetComponentInChildren<TextMeshProUGUI>();
    }

    private void RemoveCollisionListeners()
    {
        CollisionHandler.OnLandingPadEnter -= OpenWithCompletedContent;
        CollisionHandler.OnRocketCrash -= OpenWithLostContent;
    }

    private void SetupInProgressContent()
    {
        popUpText.text = "Quick Menu";

        TextMeshProUGUI bottomLeftButtonText = bottomLeftButton
            .GetComponentInChildren<TextMeshProUGUI>();
        bottomLeftButtonText.text = "Menu";

        TextMeshProUGUI bottomRightButtonText = bottomRightButton
            .GetComponentInChildren<TextMeshProUGUI>();
        bottomRightButtonText.text = "Restart";

        RemoveButtonsListeners();

        bottomLeftButton.onClick.AddListener(LevelManager.LoadMenuScene);
        bottomRightButton.onClick.AddListener(LevelManager.ReloadCurrentLevel);

        closeButton.gameObject.SetActive(true);
    }

    private void RemoveButtonsListeners()
    {
        bottomLeftButton.onClick.RemoveAllListeners();
        bottomRightButton.onClick.RemoveAllListeners();
    }

    private void OpenWithCompletedContent()
    {
        RemoveButtonsListeners();

        popUpText.text = $"Congratulations!\nIt took you {Timer.CurrentTimer:0.00} seconds!";

        bottomRightButtonText.text = "Next Level";

        closeButton.gameObject.SetActive(false);

        bottomLeftButton.onClick.AddListener(LevelManager.LoadMenuScene);
        bottomRightButton.onClick.AddListener(LevelManager.LoadNextLevel);

        if (!LevelManager.IsNextSceneLevelScene())
        {
            popUpText.text += $"\n\nYOU BEAT THE GAME!";

            bottomRightButton.gameObject.SetActive(false);
        }

        Open();
    }

    private void OpenWithLostContent()
    {
        RemoveButtonsListeners();

        popUpText.text = $"It took you {Timer.CurrentTimer:0.00}"
            + $" seconds to wreck the rocket!";

        closeButton.gameObject.SetActive(false);

        bottomLeftButton.onClick.AddListener(LevelManager.LoadMenuScene);
        bottomRightButton.onClick.AddListener(LevelManager.ReloadCurrentLevel);

        Open();
    }
}
