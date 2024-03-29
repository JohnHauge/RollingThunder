using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private Vector3 startPosition;
    private void LateUpdate()
    {
        var distance = Mathf.Clamp(target.parent.localScale.x / 5f, 1f, Mathf.Infinity);
        var targetX = target.position.x;
        var desiredPosition = new Vector3(targetX + cameraOffset.x, cameraOffset.y, cameraOffset.z);
        desiredPosition.z *= distance;
        desiredPosition.y *= distance;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        var LookAtTarget = target.position + targetOffset;
        transform.position = smoothedPosition;
        transform.LookAt(LookAtTarget);
    }
}