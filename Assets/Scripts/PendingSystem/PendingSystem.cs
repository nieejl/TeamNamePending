using UnityEngine;
using System;
using System.Timers;

public class PendingSystem : MonoBehaviour
{
    public static PendingSystem Instance { get; private set; }
    public bool IsPending() => _updateTime;
    public event Action<float, int> OnUpdateTimer;
    public event Action OnPendingTimeDone;

    [SerializeField]
    private float _updateFrequencyInSeconds = 1.0f;
    private Timer _pendingTimer;
    private TimeSpan _timeSpanFromStart;
    private DateTime _startDateTime;
    private float _totalTime;
    private float _timeCounter;
    private bool _updateTime = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (_updateTime)
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= _updateFrequencyInSeconds)
            {
                _timeCounter = 0.0f;
                UpdateTimer();
            }
        }
    }

    public void StartPendingTime(float pendingTimeInSeconds)
    {
        _timeSpanFromStart = TimeSpan.FromSeconds(pendingTimeInSeconds);
        _totalTime = pendingTimeInSeconds;
        _startDateTime = DateTime.Now;
        _updateTime = true;
        OnUpdateTimer?.Invoke(1.0f, (int)_totalTime);
        SnailAnimator.Instance.ToggleCountingAnimation(true);
    }

    public void ResetPendingTime()
    {
        SnailAnimator.Instance.PlayDamageAnimation();
        if (IsPending())
        {
            _updateTime = false;
            _timeCounter = 0.0f;
            StartPendingTime(_totalTime);
        }
    }

    private void UpdateTimer()
    {
        TimeSpan change = _timeSpanFromStart.Subtract(DateTime.Now - _startDateTime);
        int seconds = Mathf.RoundToInt((float)change.TotalSeconds);
        OnUpdateTimer?.Invoke((float)(change.TotalSeconds / _totalTime), seconds);

        if (seconds == 0)
        {
            _updateTime = false;
            OnPendingTimeDone?.Invoke();
            SnailAnimator.Instance.ToggleCountingAnimation(false);
        }
    }
}
