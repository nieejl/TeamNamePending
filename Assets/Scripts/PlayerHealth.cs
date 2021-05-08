using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    [SerializeField]
    private PlayerData _playerHealthData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void IncreaseHealth(int increaseValue)
    {
        _playerHealthData.IncreaseValue(increaseValue);
    }

    public void DecreaseHealth(int decreaseValue)
    {
        _playerHealthData.DecreaseValue(decreaseValue);
    }
}
