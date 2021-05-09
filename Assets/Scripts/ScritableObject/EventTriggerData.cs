using UnityEngine;
using System;
[CreateAssetMenu]
public class EventTriggerData : ScriptableObject
{
    public bool IsActive { get => isActive; set => isActive = value; }
    public bool ShouldReset { get => shouldReset; set => shouldReset = value; }

    [SerializeField]
    private bool isActive;
    [SerializeField]
    private bool shouldReset;

    private void OnEnable()
    {
        isActive = false;
        shouldReset = false;
    }
}