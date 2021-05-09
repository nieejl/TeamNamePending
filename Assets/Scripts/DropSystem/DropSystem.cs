using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public static DropSystem Instance;

    [SerializeField]
    private GameObject _heartPrefab;
    [SerializeField]
    private GameObject _coinPrefab;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _heartDropRate;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _coinDropRate;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void CheckIfShouldSpawnDropItemAtPosition(Vector3 position)
    {
        float coinFlip = Random.value;
        float dropRandomValue = Random.value;
        if(coinFlip > 0.5f)
        {
            if(CheckIfShouldDrop(dropRandomValue, _heartDropRate))
            {
                Quaternion quaternion = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                Instantiate(_heartPrefab, position, quaternion);
            }
        }
        else
        {
            if (CheckIfShouldDrop(dropRandomValue, _coinDropRate))
            {
                Instantiate(_coinPrefab, position, Quaternion.identity);
            }
        }
    }

    private bool CheckIfShouldDrop(float randomValue, float targetValue)
    {
        return randomValue <= targetValue;
    }
}
