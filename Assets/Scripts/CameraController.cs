using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxBounds;
    PlayerController player;
    float halfHeight;
    float halfWidth;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        SetCameraParameters();
    }

    private void SetCameraParameters()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPositionToPlayer();
    }

    private void SetCameraPositionToPlayer()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                GetXPositionClampedToBoxBoundsMinMax(),
                GetYPositionClampedToBoxBoundsMinMax(),
                transform.position.z);
        }
    }

    private float GetYPositionClampedToBoxBoundsMinMax()
    {
        return Mathf.Clamp(player.transform.position.y, boxBounds.bounds.min.y + halfHeight, boxBounds.bounds.max.y - halfHeight);
    }

    private float GetXPositionClampedToBoxBoundsMinMax()
    {
        return Mathf.Clamp(player.transform.position.x, boxBounds.bounds.min.x + halfWidth, boxBounds.bounds.max.x - halfWidth);
    }
}
