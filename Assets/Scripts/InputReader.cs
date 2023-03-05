using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    [Header("Simple movement key codes")]
    [SerializeField] private KeyCode[] thrustKeyCodes;
    [SerializeField] private KeyCode[] rotateLeftKeyCodes;
    [SerializeField] private KeyCode[] rotateRightKeyCodes;

    [Space]
    [Header("Simple movement key codes")]
    [SerializeField] private KeyCode wallPassKeyCode;

    [Header("Debuging")]
    [Space]
    [SerializeField] private bool debugging;
    [Space]
    [SerializeField] private KeyCode debug_LoadNextLevelKeyCode;

    // Simple movement keys
    public static bool ThrustKeyDown;
    public static bool RotateLeftKeyDown;
    public static bool RotateRightKeyDown;

    // Ability keys
    public static bool WallPassKeyDown;

    // Escape key
    public static bool EscapeKeyReleased;
    public static UnityAction OnEscapeKeyDown;

    // Debug keys
    public static bool Debug_LoadNextLevelKeyDown;

    private const KeyCode escapeKeyCode = KeyCode.Escape;

    private bool escapeKeyConsumed = false;

    private void Update()
    {
        ReadMovementKeys();
        ReadAbilityKeys();

        ReadEscapeKey();

        if (debugging)
            ReadDebugKeys();
    }

    private void ReadMovementKeys()
    {
        ThrustKeyDown = Input.GetKey(thrustKeyCodes[0])
            || Input.GetKey(thrustKeyCodes[1]);
        RotateLeftKeyDown = Input.GetKey(rotateLeftKeyCodes[0])
            || Input.GetKey(rotateLeftKeyCodes[1]);
        RotateRightKeyDown = Input.GetKey(rotateRightKeyCodes[0])
            || Input.GetKey(rotateRightKeyCodes[1]);
    }

    private void ReadAbilityKeys()
    {
        if (!LevelManager.IsOnLevelScene())
            return;

        WallPassKeyDown = Input.GetKey(wallPassKeyCode);
    }

    private void ReadEscapeKey()
    {
        if (Input.GetKeyDown(escapeKeyCode) && !escapeKeyConsumed)
        {
            escapeKeyConsumed = true;
            OnEscapeKeyDown?.Invoke();
        }

        EscapeKeyReleased = Input.GetKeyUp(escapeKeyCode);
        if (EscapeKeyReleased)
            escapeKeyConsumed = false;
    }

    private void ReadDebugKeys()
    {
        Debug_LoadNextLevelKeyDown = Input.GetKey(debug_LoadNextLevelKeyCode);
    }
}
