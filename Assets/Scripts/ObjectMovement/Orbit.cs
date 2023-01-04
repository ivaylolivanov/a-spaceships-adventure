using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private Vector3 rotationCenter = Vector3.zero;

    private void Update()
    {
        transform.RotateAround(
            rotationCenter,
            rotationAxis,
            travelSpeed * Time.deltaTime);
    }
}
