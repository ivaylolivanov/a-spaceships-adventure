using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public static float CurrentTimer { get; private set; }

    private bool shouldTrackTimer;

    private float showControlsPopUpDelay;

    private void OnEnable()
    {
        shouldTrackTimer = true;
        CurrentTimer = 0f;
        timerText.text = $"{CurrentTimer:0.00}";

        CollisionHandler.OnRocketCrash += StopCounter;
        CollisionHandler.OnLandingPadEnter += StopCounter;

        showControlsPopUpDelay = 0f;
    }

    private void OnDisable()
    {
        shouldTrackTimer = false;

        CollisionHandler.OnRocketCrash -= StopCounter;
        CollisionHandler.OnLandingPadEnter -= StopCounter;
    }

    private void Update()
    {
        if (!shouldTrackTimer)
            return;

        if (!PlayerProgressUtility.GetControlsShownOn1stLevel()
            && LevelManager.IsOnFirstLevel())
        {
            showControlsPopUpDelay = Time.timeSinceLevelLoad;

            return;
        }


        CurrentTimer = Time.timeSinceLevelLoad - showControlsPopUpDelay;
        timerText.text = $"{CurrentTimer:0.00}";
    }

    private void StopCounter()
        => shouldTrackTimer = false;
}
