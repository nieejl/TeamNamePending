using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController Instance { get; private set; }
    [SerializeField]
    private PlayerData _playerHealthData;
    [SerializeField]
    private PlayerData _playerCoinsData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeHealth(int changeValue)
    {
        _playerHealthData.ChangeValue(changeValue);
    }

    public void ChangeCoins(int changeValue)
    {
        _playerCoinsData.ChangeValue(changeValue);
    }
}