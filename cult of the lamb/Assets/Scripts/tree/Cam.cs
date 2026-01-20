using UnityEngine;

public class Cam : MonoBehaviour
{
    private Transform player;
    private Camera cam;
    [SerializeField] private string fadeLayerName = "TransparentFX";
    [SerializeField] private float capsuleRadius = 1f;

    private ObjectFader[] allFaders;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;

        allFaders = Object.FindObjectsByType<ObjectFader>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (player == null || cam == null) return;

        Vector3 camPos = cam.transform.position;
        Vector3 playerPos = player.position;
        Vector3 dir = playerPos - camPos;
        float distance = dir.magnitude;

        RaycastHit[] hits = Physics.CapsuleCastAll(
            camPos, camPos, capsuleRadius, dir.normalized, distance
        );

        foreach (var fader in allFaders)
            fader.DoFade = false;

        foreach (var hit in hits)
        {
            ObjectFader fader = hit.collider.GetComponent<ObjectFader>();
            if (fader != null && fader.gameObject.layer == LayerMask.NameToLayer(fadeLayerName))
            {
                fader.DoFade = true;
            }
        }
    }
}
