using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField]
    private PlayerData _playerHealthData;
    [SerializeField]
    private HorizontalLayoutGroup _playerHealthContainer;
    [SerializeField]
    private GameObject _playerHealthIconPrefab;
    private GameObject[] _playerHelthIcons;

    [Header("Coins")]
    [SerializeField]
    private PlayerData _playerCoins;
    [SerializeField]
    private TMP_Text _coinsText;

    [Header("Timer")]
    [SerializeField]
    private RectTransform _timerContainer;
    [SerializeField]
    private Image _timer;
    [SerializeField]
    private TMP_Text _timerText;

    [SerializeField]
    private Image[] _weaponIcons;

    [SerializeField]
    private TMP_Text RestartText;


    public static UIController Instance;
    
    private void Awake()
    {
        Instance = this;
        InitializeHealthIcons();
        _playerHealthData.ChangedToValue += UpdateHealthIcons;

        InitializeCoinsAmount();
        _playerCoins.ChangedToValue += UpdatePlayerCoins;
    }

    private void Start()
    {
        PendingSystem.Instance.OnUpdateTimer += UpdateTimer;
        PendingSystem.Instance.OnPendingTimeDone += () => { EnableTimerContainer(false); };
        EnableTimerContainer(false);

        Inventory.OnWeaponChange += ChangeWeapon;
        ChangeWeapon(0);
    }

    public void ShowRestartGameText()
    {
        RestartText.text = "Press space to play again.";
    }

    public void HideRestartText()
    {
        RestartText.text = "";
    }

    private void InitializeHealthIcons()
    {
        int maxNumber = _playerHealthData.MaximumAmount;
        _playerHelthIcons = new GameObject[maxNumber];
        for (int i = 0; i < maxNumber; i++)
        {
            _playerHelthIcons[i] = Instantiate(_playerHealthIconPrefab, _playerHealthContainer.transform);
            _playerHelthIcons[i].SetActive(i < _playerHealthData.StartAmount);
        }
    }

    private void UpdateHealthIcons(int numberOfhealthPoints)
    {
        int maxNumber = _playerHealthData.MaximumAmount;
        for (int i = 0; i < maxNumber; i++)
        {
            _playerHelthIcons[i].SetActive(i < numberOfhealthPoints);
        }
    }

    private void ChangeWeapon(int weaponIndex)
    {
        int numberOfWeapons = _weaponIcons.Length;
        for (int i = 0; i < numberOfWeapons; i++)
        {
            _weaponIcons[i].gameObject.SetActive(i == weaponIndex);
        }
    }

    private void InitializeCoinsAmount()
    {
        UpdatePlayerCoins(_playerCoins.StartAmount);
    }

    private void UpdatePlayerCoins(int numberOfCoins)
    {
        _coinsText.text = numberOfCoins.ToString();
    }

    private void OnDestroy()
    {
        _playerHealthData.ChangedToValue -= UpdateHealthIcons;
        _playerCoins.ChangedToValue -= UpdatePlayerCoins;

        Inventory.OnWeaponChange -= ChangeWeapon;
    }

    private void UpdateTimer(float percentage, int seconds)
    {
        _timer.fillAmount = percentage;
        _timerText.text = seconds.ToString();
        EnableTimerContainer(true);
    }

    private void EnableTimerContainer(bool enable)
    {
        _timerContainer.gameObject.SetActive(enable);
    }

}
