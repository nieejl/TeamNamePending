using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static readonly PlayerHealth Instance = new PlayerHealth();
    [SerializeField]
    private PlayerData _playerHealthData;

    public void IncreaseHealth(int increaseValue)
    {
        _playerHealthData.IncreaseValue(increaseValue);
    }

    public void DecreaseHealth(int decreaseValue)
    {
        _playerHealthData.DecreaseValue(decreaseValue);
    }
}
