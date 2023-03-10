using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectionPopUp : PopUp
{
    [Header("Organizational transform")]
    [SerializeField] private Transform levelItemsParent;

    [Header("Cosmetics")]
    [SerializeField] private float levelItemTextSize;
    [SerializeField] private Color levelUnlockedColor;
    [SerializeField] private Color levelLockedColor;

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeLevelItems();
    }

    private void InitializeLevelItems()
    {
        int levelsCount = LevelManager.GetLevelsCount();
        for (int i = 0; i < levelsCount; ++i)
        {
            int levelBuildIndex = i + 1;

            InitializeLevelItem(levelBuildIndex);
        }
    }

    private void InitializeLevelItem(int levelBuildIndex)
    {
        int highestLevelCompleted = PlayerProgressUtility
            .GetHighestLevelCompleted();

        GameObject levelItem = CreateItemLevel(levelBuildIndex);

        bool isLevelLocked = highestLevelCompleted < levelBuildIndex;
        Color levelColor = levelUnlockedColor;
        if (isLevelLocked)
            levelColor = levelLockedColor;

        SetLevelItemText(levelItem, levelBuildIndex, levelColor);

        if (isLevelLocked)
            return;

        AddButtonToLevelItem(levelItem, levelBuildIndex);
    }

    private GameObject CreateItemLevel(int levelIndex)
    {
        GameObject levelItem = new GameObject($"LevelItem-{levelIndex}");

        Transform levelItemTransform = levelItem.transform;
        levelItemTransform.SetParent(levelItemsParent);
        levelItemTransform.localScale = Vector3.one;

        return levelItem;
    }

    private void SetLevelItemText(GameObject levelItem, int levelIndex, Color color)
    {
        TextMeshProUGUI levelItemText = levelItem
            .AddComponent<TextMeshProUGUI>();
        levelItemText.text = $"{levelIndex}";
        levelItemText.color = color;
        levelItemText.fontSize = levelItemTextSize;
    }

    private void AddButtonToLevelItem(GameObject levelItem, int levelIndex)
    {
        Button levelItemButton = levelItem.AddComponent<Button>();
        levelItemButton.onClick.AddListener(() => LevelManager.LoadLevel(levelIndex));
    }
}
