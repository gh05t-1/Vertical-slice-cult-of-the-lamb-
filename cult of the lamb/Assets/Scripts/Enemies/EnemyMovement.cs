using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 3f;
    public event Action OnStartPatrol;
    public event Action OnStopPatrol;
    public event Action OnStartChase;
    public event Action OnStopChase;
    public event Action<Vector3> OnDirectionChanged;

    protected void TriggerStartPatrol() => OnStartPatrol?.Invoke();
    protected void TriggerStopPatrol() => OnStopPatrol?.Invoke();
    protected void TriggerStartChase() => OnStartChase?.Invoke();
    protected void TriggerStopChase() => OnStopChase?.Invoke();
    protected void TriggerDirection(Vector3 dir) => OnDirectionChanged?.Invoke(dir);

    protected void UpdateFlip(Vector3 moveDir)
    {
        if (moveDir.sqrMagnitude < 0.001f) return;
        TriggerDirection(moveDir);
    }
}
