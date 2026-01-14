
using UnityEngine;

public class Rotate2DToMouseZ : MonoBehaviour
{
    [Header("References")]
    public Camera cam;                     // If null, defaults to Camera.main

    [Header("Tuning")]
    [Tooltip("Degrees to add after computing the angle (use if your sprite isn't facing +X).")]
    public float angleOffsetDegrees = 0f;  // e.g., 90 if sprite's up should point to mouse
    [Tooltip("Smoothing factor; 0 = instant snap.")]
    public float rotationSmoothing = 0f;   // e.g., 15 for smooth

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) return;

        // Get world position of the mouse at the object's depth
        Vector3 mouseWorld = GetMouseWorldAtObjectDepth(transform.position, cam);

        // Compute direction in the XY plane (ignore Z)
        Vector2 dir = (Vector2)(mouseWorld - transform.position);
        if (dir.sqrMagnitude < 1e-6f) return;

        // Angle in degrees, 0° pointing +X, increasing CCW (Unity's default)
        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffsetDegrees;

        // Build a pure Z-axis rotation
        Quaternion target = Quaternion.Euler(0f, 0f, angleDeg);

        // Apply with optional smoothing
        if (rotationSmoothing > 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSmoothing);
        }
        else
        {
            transform.rotation = target;
        }
    }

    /// <summary>
    /// Converts mouse position to a world point that lies at the same depth as 'objectPos'
    /// relative to the camera. Works for both perspective and orthographic cameras.
    /// </summary>
    private static Vector3 GetMouseWorldAtObjectDepth(Vector3 objectPos, Camera cam)
    {
        if (cam.orthographic)
        {
            // For orthographic cameras, ScreenToWorldPoint ignores z and uses camera's near plane,
            // so set z to the object's plane by copying the object's z after conversion.
            Vector3 mousePos = Input.mousePosition;
            Vector3 world = cam.ScreenToWorldPoint(mousePos);
            world.z = objectPos.z;
            return world;
        }
        else
        {
            // For perspective, we must pass the correct distance from the camera to the object
            float distance = Vector3.Dot(objectPos - cam.transform.position, cam.transform.forward);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = distance; // depth along view direction
            return cam.ScreenToWorldPoint(mousePos);
        }
    }
}

