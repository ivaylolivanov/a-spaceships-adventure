using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlsPopUp : PopUp
{
    private const int LEFT_DIRECTION  = -1;
    private const int RIGHT_DIRECTION = 1;

    [Header("Common")]
    [Header("Periods")]
    [SerializeField] private float buttonColorAlternatePeriod;
    [SerializeField] private float buttonTextSwapPeriod;
    [Space(20)]
    [Header("Colors")]
    [SerializeField] private Color buttonReleasedColor;
    [SerializeField] private Color buttonPressedColor;

    [Space(30)]
    [Header("Button images")]
    [SerializeField] private Image thrustButtonImage;
    [SerializeField] private Image rotateLeftButtonImage;
    [SerializeField] private Image rotateRightButtonImage;

    [Space(20)]
    [Header("Button texts")]
    [SerializeField] private TextMeshProUGUI thrustButtonText;
    [SerializeField] private TextMeshProUGUI rotateLeftButtonText;
    [SerializeField] private TextMeshProUGUI rotateRightButtonText;

    private int curentRotationDirection;

    private Coroutine colorsCoroutine;
    private Coroutine textsCoroutine;

    private bool firstLevelInitialControlsDisplayed;

    protected override void OnEnable()
    {
        base.OnEnable();

        curentRotationDirection = RIGHT_DIRECTION;
    }

    protected override void Start()
    {
        base.Start();

        firstLevelInitialControlsDisplayed =
            !PlayerProgressUtility.GetControlsShownOn1stLevel()
            && LevelManager.IsOnFirstLevel();
        if (firstLevelInitialControlsDisplayed)
            Open();
    }

    public override void Open()
    {
        base.Open();
        StartAnimations();
    }

    public override void Close()
    {
        base.Close();
        StopAnimations();

        if (firstLevelInitialControlsDisplayed)
            UpdateControlsShownInProgression();
    }

    private void StartAnimations()
    {
        colorsCoroutine = StartCoroutine(SwapButtonColors());
        textsCoroutine  = StartCoroutine(SwapButtonTexts());
    }

    private void StopAnimations()
    {
        if (colorsCoroutine != null)
            StopCoroutine(colorsCoroutine);

        if (textsCoroutine != null)
            StopCoroutine(textsCoroutine);
    }

    private void UpdateControlsShownInProgression()
    {
        PlayerProgressUtility.SetControlsShownOn1stLevel();
        closeButton.onClick.RemoveListener(UpdateControlsShownInProgression);
    }

    private IEnumerator SwapButtonColors()
    {
        for (;;)
        {
            thrustButtonImage.color = buttonPressedColor;
            if (curentRotationDirection == LEFT_DIRECTION)
                rotateLeftButtonImage.color = buttonPressedColor;
            else
                rotateRightButtonImage.color = buttonPressedColor;

            yield return new WaitForSeconds(buttonColorAlternatePeriod);

            thrustButtonImage.color = buttonReleasedColor;
            if (curentRotationDirection == LEFT_DIRECTION)
            {
                rotateLeftButtonImage.color = buttonReleasedColor;
                curentRotationDirection = RIGHT_DIRECTION;
            }
            else
            {
                rotateRightButtonImage.color = buttonReleasedColor;
                curentRotationDirection = LEFT_DIRECTION;
            }

            yield return new WaitForSeconds(buttonColorAlternatePeriod);
        }
    }

    private IEnumerator SwapButtonTexts()
    {
        for (;;)
        {
            thrustButtonText.text = "Space";
            rotateLeftButtonText.text = "A";
            rotateRightButtonText.text = "D";

            yield return new WaitForSeconds(buttonTextSwapPeriod);

            thrustButtonText.text = "Numpad 0";
            rotateLeftButtonText.text = "<-";
            rotateRightButtonText.text = "->";

            yield return new WaitForSeconds(buttonTextSwapPeriod);
        }
    }
}
