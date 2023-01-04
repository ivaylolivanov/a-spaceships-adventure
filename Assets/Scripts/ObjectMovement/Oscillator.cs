using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;

    [SerializeField] private bool CanBeStopped;

    // Constant value of 6.283
    private const float TAU = Mathf.PI * 2;

    private Vector3 startingPosition;

    private void OnEnable()
    {
        startingPosition = transform.position;

        CollisionHandler.OnLandingPadEnter += StopOscillating;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon)
            return;

        // Continually growing over time
        float cycles = Time.time / period;

        // Going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * TAU);

        // Recalculated from 0 to 1 so it is cleaner
        float movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

    private void StopOscillating()
    {
        if (!CanBeStopped)
            return;

        period = 0;
    }
}
