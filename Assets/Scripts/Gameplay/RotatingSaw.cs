using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSaw : MonoBehaviour
{
    // Store value for speed of rotation
    [SerializeField] private float rotatingSpeed;

    private void Update()
    {
        transform.Rotate(0, 0, 360 * rotatingSpeed * Time.deltaTime);
        // Sprite of the saw is rotated 360 degrees
        // At a speed of rotatingSpeed
        // In every frame.
    }
}
