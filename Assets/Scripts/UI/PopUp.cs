using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class PopUp : MonoBehaviour
{
    [SerializeField] protected bool toggleable;

    [Space]
    [SerializeField] protected Button closeButton;

    public static UnityAction<PopUp> OnPopUpCreated;
    public static UnityAction<PopUp> OnPopUpOpened;
    public static UnityAction<PopUp> OnPopUpClosed;

    public bool Toggleable => toggleable;

    protected virtual void OnEnable()
    {
        closeButton.onClick.AddListener(Close);
        Close();
    }

    protected virtual void Start()
        => OnPopUpCreated?.Invoke(this);

    public virtual void Toggle()
    {
        if (!toggleable)
            return;

        if (IsPopUpOpen())
            Close();
        else
            Open();
    }

    public virtual void Close()
    {
        OnPopUpClosed?.Invoke(this);

        transform.localScale = Vector3.zero;
    }

    public virtual void Open()
    {
        OnPopUpOpened?.Invoke(this);

        transform.localScale = Vector3.one;
    }

    private bool IsPopUpOpen()
        => transform.localScale == Vector3.one;
}
