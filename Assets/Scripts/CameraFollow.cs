using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标物体
    public Vector3 offset; // 相机相对于目标的偏移量
    public float smoothSpeed = 0.125f; // 相机移动的平滑速度
    public Vector2 Range = new Vector3(-4.3f, 4.3f);

    private Vector3 movePosition;

    private void Start()
    {
        movePosition = transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            movePosition.x = Mathf.Clamp(smoothedPosition.x, Range.x, Range.y);
            transform.position = movePosition;
        }
    }
}