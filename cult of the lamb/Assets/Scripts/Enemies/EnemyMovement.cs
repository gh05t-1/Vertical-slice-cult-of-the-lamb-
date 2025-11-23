using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 3f;
    public event Action OnStartPatrol;
    public event Action OnStopPatrol;
    public event Action OnStartChase;
    public event Action OnStopChase;

    protected void TriggerStartPatrol() => OnStartPatrol?.Invoke();
    protected void TriggerStopPatrol() => OnStopPatrol?.Invoke();
    protected void TriggerStartChase() => OnStartChase?.Invoke();
    protected void TriggerStopChase() => OnStopChase?.Invoke();
}