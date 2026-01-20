
using UnityEngine;

public class Rotate2DToMouseZ : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;


    [SerializeField] private float angleOffsetDegrees = 0f;
    [SerializeField] private float rotationSmoothing = 0f;

    [SerializeField] private int attackMouseButton = 0;
    [SerializeField] private string attackTriggerName = "Attack";

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) return;

        // Rotation
        Vector3 mouseWorld = GetMouseWorldAtObjectDepth(transform.position, cam);
        Vector2 dir = (Vector2)(mouseWorld - transform.position);


        if (dir.sqrMagnitude < 1e-6f) return;


        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffsetDegrees;
        Quaternion target = Quaternion.Euler(0f, 0f, angleDeg);

        if (rotationSmoothing > 0f)
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSmoothing);
        else
            transform.rotation = target;

        // Attack input
        if (Input.GetMouseButtonDown(attackMouseButton))
        {
            Debug.Log("attack");
            if (animator != null && !string.IsNullOrEmpty(attackTriggerName))
            {
                animator.SetTrigger(attackTriggerName);
            }
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
            Vector3 mousePos = Input.mousePosition;
            Vector3 world = cam.ScreenToWorldPoint(mousePos);
            world.z = objectPos.z;
            return world;
        }
        else
        {
            float distance = Vector3.Dot(objectPos - cam.transform.position, cam.transform.forward);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = distance;
            return cam.ScreenToWorldPoint(mousePos);
        }
    }
}
