using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    [Header("Simple movement key codes")]
    [SerializeField] private KeyCode thrustKeyCode;
    [SerializeField] private KeyCode rotateLeftKeyCode;
    [SerializeField] private KeyCode rotateRightKeyCode;

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

    // Debug keys
    public static bool Debug_LoadNextLevelKeyDown;

    private const KeyCode escapeKeyCode = KeyCode.Escape;

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
        ThrustKeyDown = Input.GetKey(thrustKeyCode);
        RotateLeftKeyDown = Input.GetKey(rotateLeftKeyCode);
        RotateRightKeyDown = Input.GetKey(rotateRightKeyCode);
    }

    private void ReadAbilityKeys()
    {
        if (!LevelManager.IsOnLevelScene())
            return;

        WallPassKeyDown = Input.GetKey(wallPassKeyCode);
    }

    private void ReadEscapeKey()
    {
        EscapeKeyReleased = Input.GetKeyUp(escapeKeyCode);
    }

    private void ReadDebugKeys()
    {
        Debug_LoadNextLevelKeyDown = Input.GetKey(debug_LoadNextLevelKeyCode);
    }
}
