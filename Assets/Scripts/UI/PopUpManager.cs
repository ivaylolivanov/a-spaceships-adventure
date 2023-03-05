using UnityEngine;
using System.Collections.Generic;

public class PopUpManager : MonoBehaviour
{
    private HashSet<PopUp> availablePopUps;
    private LinkedList<PopUp> activePopUps;

    private bool escapeKeyConsumed;

    private void Awake()
    {
        InputReader.OnEscapeKeyDown += OnEscapeKeyPressed;

        PopUp.OnPopUpCreated += CachePopUp;
        PopUp.OnPopUpOpened += AddPopUp;
        PopUp.OnPopUpClosed += RemovePopUp;
    }

    private void OnEnable()
    {
        activePopUps = new LinkedList<PopUp>();
        availablePopUps = new HashSet<PopUp>();

        escapeKeyConsumed = true;
    }

    private void OnDisable()
    {
        activePopUps.Clear();
        availablePopUps.Clear();
    }

    private void Update()
    {
        if (escapeKeyConsumed)
            return;

        HandleClosingPopUps();
        HandleTogglingPopUps();
    }

    private void HandleClosingPopUps()
    {
        if (activePopUps.Count <= 0)
            return;

        CloseLastPopUp();
        escapeKeyConsumed = true;
        return;
    }

    private void HandleTogglingPopUps()
    {
        foreach (var popUp in availablePopUps)
        {
            if (escapeKeyConsumed)
                return;

            if (!popUp.Toggleable)
                continue;

            popUp.Toggle();
            escapeKeyConsumed = true;
            return;
        }
    }

    private void OnEscapeKeyPressed()
        => escapeKeyConsumed = false;

    private void CloseLastPopUp()
        => activePopUps.Last.Value.Close();

    private void CachePopUp(PopUp popUp)
    {
        if (availablePopUps.Contains(popUp))
            return;

        availablePopUps.Add(popUp);
    }

    private void AddPopUp(PopUp popUp)
    {
        if (activePopUps.Contains(popUp))
            RemovePopUp(popUp);

        activePopUps.AddLast(popUp);
        popUp.transform.SetAsLastSibling();
    }

    private void RemovePopUp(PopUp popUp)
        => activePopUps.Remove(popUp);
}
