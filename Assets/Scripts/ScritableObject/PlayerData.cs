using UnityEngine;
using System;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int CurrentValue { get => _currentValue; private set => _currentValue = value; }
    public int StartAmount => _startAmount;
    public int MaximumAmount => _maximumAmount;

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

    public void ChangeValue(int changeValue)
    {
        _currentValue += changeValue;
        if (_currentValue >= _maximumAmount)
        {
            _currentValue = _maximumAmount;
        }

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