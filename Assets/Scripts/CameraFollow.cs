using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标物体
    public Vector3 offset; // 相机相对于目标的偏移量
    public float smoothSpeed = 0.125f; // 相机移动的平滑速度

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}