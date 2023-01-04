using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    public static UnityAction OnRocketCrash;
    public static UnityAction OnLandingPadEnter;

    [SerializeField] private ParticleSystem landingParticles;
    [SerializeField] private ParticleSystem crashParticles;

    private bool hasCrashed = false;
    private bool hasLanded = false;

    private const string friendly = "Friendly";
    private const string finish   = "Finish";

    private void OnCollisionEnter(Collision other)
    {
        if (hasLanded || hasCrashed) return;

        if (other.gameObject.tag == finish)
            HandleCollisionWithFinish();
        else if (other.gameObject.tag != friendly)
            HandleCollisionWithObstacles();
    }

    private void HandleCollisionWithFinish()
    {
        landingParticles.Play();

        UpdatePlayerProgression();

        OnLandingPadEnter?.Invoke();

        hasLanded = true;
    }

    private void HandleCollisionWithObstacles()
    {
        crashParticles.Play();
        OnRocketCrash?.Invoke();

        hasCrashed = true;
    }

    private void UpdatePlayerProgression()
    {
        int sceneIndex = LevelManager.GetCurrentSceneIndex();
        ++sceneIndex;
        if (LevelManager.IsSceneIndexValidLevelIndex(sceneIndex))
            PlayerProgressUtility.UpdateLevelCompleted(sceneIndex);
    }
}
