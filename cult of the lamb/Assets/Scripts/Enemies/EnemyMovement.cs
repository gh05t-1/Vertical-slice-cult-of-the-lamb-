using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 3f;
    public static event Action OnStartPatrol;
    public static event Action OnStopPatrol;
    public static event Action OnStartChase;
    public static event Action OnStopChase;
    public static bool BlockAllMovement = false;


    protected void TriggerStartPatrol() => OnStartPatrol?.Invoke();
    protected void TriggerStopPatrol() => OnStopPatrol?.Invoke();
    protected void TriggerStartChase() => OnStartChase?.Invoke();
    protected void TriggerStopChase() => OnStopChase?.Invoke();
}