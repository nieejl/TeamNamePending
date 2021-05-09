using UnityEngine;
using System;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int CurrentValue { get; private set; }
    public int StartAmount => _startAmount;
    public int MaximumAmount => _maximumAmount;

    public event Action<int> ChangedToValue;
    public event Action ValueReachedZero;

    [SerializeField]
    private int _startAmount;
    [SerializeField]
    private int _maximumAmount;

    private void OnEnable()
    {
        ResetValue();
    }

    public void ChangeValue(int changeValue)
    {
        CurrentValue += changeValue;
        if (CurrentValue >= _maximumAmount)
        {
            CurrentValue = _maximumAmount;
        }

        if (CurrentValue <= 0)
        {
            CurrentValue = 0;
            ValueReachedZero?.Invoke();
        }
        else
        {
            ChangedToValue?.Invoke(CurrentValue);
        }
    }

    public void ResetValue()
    {
        CurrentValue = _startAmount;
    }
}