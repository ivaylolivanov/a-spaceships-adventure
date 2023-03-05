using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float rotationThrust;

    [Space]
    [Header("Sound effects")]
    [Header("Movement")]
    [SerializeField] private AudioClip thrustSfx;
    [SerializeField] private float thrustSfxVolume;
    [Header("Crash")]
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private float crashSfxVolume;
    [Header("Landing")]
    [SerializeField] private AudioClip landingPadSfx;
    [SerializeField] private float landingPadSfxVolume;

    [Space]
    [Header("Particle effects")]
    [SerializeField] private ParticleSystem[] leftBoostersParticles;
    [SerializeField] private ParticleSystem[] rightBoostersParticles;
    [SerializeField] private ParticleSystem   mainBoosterParticles;

    private Rigidbody rb;
    private AudioSource audioSource;

    private bool canMove = true;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        canMove = true;

        CollisionHandler.OnRocketCrash += DisableMovement;
        CollisionHandler.OnLandingPadEnter += RocketLanded;
    }

    private void OnDisable()
    {
        CollisionHandler.OnRocketCrash -= DisableMovement;
        CollisionHandler.OnLandingPadEnter -= RocketLanded;

        canMove = false;
    }

    private void Update()
    {
        if (!PlayerProgressUtility.GetControlsShownOn1stLevel()
            && LevelManager.IsOnFirstLevel())
            return;

        if (!canMove)
            return;

        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (InputReader.ThrustKeyDown)
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if (!mainBoosterParticles.isPlaying)
                mainBoosterParticles.Play();

            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(thrustSfx, thrustSfxVolume);
        }
        else
        {
            audioSource.Stop();
            mainBoosterParticles.Stop();
            return;
        }
    }

    private void ProcessRotation()
    {
        if (InputReader.RotateRightKeyDown)
            RotateRight();
        else if (InputReader.RotateLeftKeyDown)
            RotateLeft();
        else
            StopBoostersParticles();
    }

    private void RotateRight()
    {
        ApplyRotation(Vector3.forward);
        foreach (var particle in rightBoostersParticles)
            particle.Play();
    }

    private void RotateLeft()
    {
        ApplyRotation(-Vector3.forward);
        foreach (var particle in leftBoostersParticles)
            particle.Play();
    }

    private void StopBoostersParticles()
    {
        foreach (var particle in rightBoostersParticles)
            particle.Stop();

        foreach (var particle in leftBoostersParticles)
            particle.Stop();
    }

    private void ApplyRotation(Vector3 direction)
    {
        // Freezing rotation so we can manually rotate
        rb.freezeRotation = true;

        transform.Rotate(direction * rotationThrust * Time.deltaTime);

        // Un freezing rotation because we are no longer manually rotating
        rb.freezeRotation = false;
    }

    private void DisableMovement()
    {
        audioSource.Stop();

        audioSource.PlayOneShot(crashSfx, crashSfxVolume);

        canMove = false;
    }

    private void RocketLanded()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(landingPadSfx, landingPadSfxVolume);

        canMove = false;
    }
}
