using UnityEngine;
using System;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int CurrentValue { get; private set; }
    public int StartAmount { get { return _startAmount; } }
    public int MaximumAmount { get { return _maximumAmount; } }

    public event Action<int> ChangedToValue;
    public event Action ValueReachedZero;

    [SerializeField]
    private int _startAmount;
    [SerializeField]
    private int _maximumAmount;

    private int _currentValue;

    private void OnEnable()
    {
        _currentValue = _startAmount;
    }

    public void IncreaseValue(int increaseValue)
    {
        _currentValue += increaseValue;
        if (_currentValue >= _maximumAmount)
        {
            _currentValue = _maximumAmount;
        }
        ChangedToValue?.Invoke(_currentValue);
    }

    public void DecreaseValue(int decreaseValue)
    {
        _currentValue -= decreaseValue;
        if (_currentValue <= 0)
        {
            _currentValue = 0;
            ValueReachedZero?.Invoke();
        }
        else
        {
            ChangedToValue?.Invoke(_currentValue);
        }
    }
}