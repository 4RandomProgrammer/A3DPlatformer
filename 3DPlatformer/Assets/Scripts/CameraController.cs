using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float sensitivity = 5f;
    public float minYAngle = -30f;
    public float maxYAngle = 60f;

    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
       
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        transform.position = player.position + rotation * offset;

        transform.LookAt(player);
    }
}
