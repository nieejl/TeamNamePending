using UnityEngine;
using System;
[CreateAssetMenu]
public class EventTriggerData : ScriptableObject
{
    public bool IsActive { get => isActive; set => isActive = value; }
    public bool ShouldReset { get => _shouldReset; set => _shouldReset = value; }

    public event Action<int> ChangedToValue;
    public event Action ValueReachedZero;

    [SerializeField]
    private bool isActive;
    [SerializeField]
    private bool _shouldReset;

    private void OnEnable()
    {
        isActive = false;
        _shouldReset = false;
    }
}