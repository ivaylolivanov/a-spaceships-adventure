using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public static float CurrentTimer { get; private set; }

    private bool shouldTrackTimer;

    private void OnEnable()
    {
        shouldTrackTimer = true;
        CurrentTimer = 0f;

        CollisionHandler.OnRocketCrash += StopCounter;
        CollisionHandler.OnLandingPadEnter += StopCounter;
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

        CurrentTimer = Time.timeSinceLevelLoad;
        timerText.text = $"{CurrentTimer:0.00}";
    }

    private void StopCounter()
        => shouldTrackTimer = false;
}
