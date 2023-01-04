using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStatusPopUp : MonoBehaviour
{
    [SerializeField] private Button centerButton;
    [SerializeField] private Button bottomLeftButton;
    [SerializeField] private Button bottomRightButton;

    [SerializeField] private TextMeshProUGUI popUpText;

    private bool canOpenNormalPopUp;

    private TextMeshProUGUI centerButtonText;
    private TextMeshProUGUI bottomLeftButtonText;
    private TextMeshProUGUI bottomRightButtonText;

    private void OnEnable()
    {
        canOpenNormalPopUp = true;
        CollisionHandler.OnLandingPadEnter += OpenLevelCompletedPopUp;
        CollisionHandler.OnRocketCrash += OpenLevelLostPopUp;

        centerButtonText = centerButton
            .GetComponentInChildren<TextMeshProUGUI>();

        bottomLeftButtonText = bottomLeftButton
            .GetComponentInChildren<TextMeshProUGUI>();

        bottomRightButtonText = bottomRightButton
            .GetComponentInChildren<TextMeshProUGUI>();

        FillInDefaultPopUpContent();
        ClosePopUp();
    }

    private void OnDisable()
    {
        RemoveButtonsListeners();

        canOpenNormalPopUp = true;
        CollisionHandler.OnLandingPadEnter -= OpenLevelCompletedPopUp;
        CollisionHandler.OnRocketCrash -= OpenLevelLostPopUp;
    }

    private void Update()
    {
        if (!InputReader.EscapeKeyReleased)
            return;

        if (canOpenNormalPopUp)
            TogglePopUp();
    }

    private void FillInDefaultPopUpContent()
    {
        popUpText.text = "Quick Menu";

        TextMeshProUGUI centerButtonText = centerButton
            .GetComponentInChildren<TextMeshProUGUI>();
        centerButtonText.text = "Continue";

        TextMeshProUGUI bottomLeftButtonText = bottomLeftButton
            .GetComponentInChildren<TextMeshProUGUI>();
        bottomLeftButtonText.text = "Menu";

        TextMeshProUGUI bottomRightButtonText = bottomRightButton
            .GetComponentInChildren<TextMeshProUGUI>();
        bottomRightButtonText.text = "Restart";

        RemoveButtonsListeners();

        centerButton.onClick.AddListener(ClosePopUp);
        bottomLeftButton.onClick.AddListener(LevelManager.LoadMenuScene);
        bottomRightButton.onClick.AddListener(LevelManager.ReloadCurrentLevel);
    }

    private void RemoveButtonsListeners()
    {
        centerButton.onClick.RemoveAllListeners();
        bottomLeftButton.onClick.RemoveAllListeners();
        bottomRightButton.onClick.RemoveAllListeners();
    }

    private void TogglePopUp()
    {
        bool isPopUpOpen = transform.localScale == Vector3.one;
        if (isPopUpOpen)
            ClosePopUp();
        else
            OpenPopUp();
    }

    private void OpenPopUp()
    {
        transform.localScale = Vector3.one;
        Debug.Log("Opening pop up");
    }

    private void ClosePopUp()
    {
        transform.localScale = Vector3.zero;
        Debug.Log("Closing pop up");
    }

    private void OpenLevelCompletedPopUp()
    {
        canOpenNormalPopUp = false;

        popUpText.text = $"You have completed the level for {Timer.CurrentTimer:0.00} seconds!";

        TextMeshProUGUI centerButtonText = centerButton
            .GetComponentInChildren<TextMeshProUGUI>();
        centerButtonText.text = "Next Level";

        RemoveButtonsListeners();

        centerButton.onClick.AddListener(LevelManager.LoadNextLevel);
        bottomLeftButton.onClick.AddListener(LevelManager.LoadMenuScene);
        bottomRightButton.onClick.AddListener(LevelManager.ReloadCurrentLevel);

        OpenPopUp();
    }

    private void OpenLevelLostPopUp()
    {
        canOpenNormalPopUp = false;

        popUpText.text = $"You flew {Timer.CurrentTimer:0.00}"
            + $" seconds. And yet the rocket is a wreck!";

        TextMeshProUGUI centerButtonText = centerButton
            .GetComponentInChildren<TextMeshProUGUI>();

        centerButtonText.text = "Next Level";

        RemoveButtonsListeners();

        Debug.Log($"bottom left is null: {bottomLeftButton == null}");

        centerButton.gameObject.SetActive(false);
        bottomLeftButton.onClick.AddListener(LevelManager.LoadMenuScene);
        bottomRightButton.onClick.AddListener(LevelManager.ReloadCurrentLevel);

        OpenPopUp();
    }
}
