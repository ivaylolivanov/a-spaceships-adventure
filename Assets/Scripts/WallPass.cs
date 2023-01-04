using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPass : MonoBehaviour
{
    [SerializeField] private float duration;

    private Collider bodyCollider;

    private Rigidbody rb;
    private Coroutine wallPassCoroutine;

    private void OnEnable()
    {
        bodyCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (InputReader.WallPassKeyDown)
        {
            wallPassCoroutine = StartCoroutine(
                DisableColliderForDuration());
        }
        else if (wallPassCoroutine != null)
        {
            StopCoroutine(wallPassCoroutine);
            rb.detectCollisions = true;
        }
    }

    private IEnumerator DisableColliderForDuration()
    {
        rb.detectCollisions = false;

        yield return new WaitForSeconds(duration);

        rb.detectCollisions = true;
    }
}
