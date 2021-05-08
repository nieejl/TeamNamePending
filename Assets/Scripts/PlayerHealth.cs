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

    public void ChangeHealth(int changeValue)
    {
        _playerHealthData.ChangeValue(changeValue);
    }
}
