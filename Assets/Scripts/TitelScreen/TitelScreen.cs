using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitelScreen : MonoBehaviour
{
    [SerializeField]
    private Button _startGameButton;
    [SerializeField]
    private Button _controlsButton;
    [SerializeField]
    private Button _optionButton;
    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private GameObject _controls;
    [SerializeField]
    private Button _closeControls;

    [SerializeField]
    private GameObject _options;
    [SerializeField]
    private Button _closeOption;

    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _sfxSlider;

    [SerializeField] 
    private TMP_Text _itemDurabilityText;
    
    [SerializeField]
    private Slider _itemDurabilityLevelSlider;

    [SerializeField] private ItemDurabilityData DurabilityData;
    FMOD.Studio.EventInstance musicEvent;

    private void Awake()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicStateParameter", 0.0f);
        _startGameButton.onClick.AddListener(ClickedStartGameButton);
        _controlsButton.onClick.AddListener(() => { EnableControlsWindow(true); });
        _closeControls.onClick.AddListener(() => { EnableControlsWindow(false); });
        _optionButton.onClick.AddListener(() => { EnableOptionWindow(true); });
        _closeOption.onClick.AddListener(() => { EnableOptionWindow(false); });

        _musicSlider.value = 0.7f;
        _sfxSlider.value = 0.7f;
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);

        _itemDurabilityLevelSlider.value = 0.25f;
        if (DurabilityData.AttacksTillDestroyed <= 1)
        {
            DurabilityData.SetValue(25);
        }
        _itemDurabilityText.text = "Item Durability: 25";
        _itemDurabilityLevelSlider.onValueChanged.AddListener(ChangeDurabilityLevel);

        _exitButton.onClick.AddListener(ClickedExitButton);

        EnableControlsWindow(false);
        EnableOptionWindow(false);

        //FMOD MUSIC START UPON MENU/GAME START
        musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Music/musicEvent");
        musicEvent.start();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicStateParameter", 0f);
    }

    private void ClickedStartGameButton()
    {
        SceneManager.LoadScene("Game");
    }

    private void ClickedExitButton()
    {
        Application.Quit();
    }

    private void ChangeDurabilityLevel(float value)
    {
        int attacksBeforeBreaking = (int) (value * 100);
        DurabilityData.SetValue(attacksBeforeBreaking);
        _itemDurabilityText.text = "Item Durability: " + attacksBeforeBreaking;
    }

    private void EnableControlsWindow(bool enable)
    {
        _controls.SetActive(enable);
    }

    private void EnableOptionWindow(bool enable)
    {
        _options.SetActive(enable);
    }

    private void ChangeMusicVolume(float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicVolume", value);
    }

    private void ChangeSFXVolume(float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("sfxVolume", value);
    }
}
