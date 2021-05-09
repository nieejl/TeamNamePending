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



    private void Awake()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicStateParameter", 0.0f);
        _startGameButton.onClick.AddListener(ClickedStartGameButton);
        _controlsButton.onClick.AddListener(() => { EnableControlsWindow(true); });
        _closeControls.onClick.AddListener(() => { EnableControlsWindow(false); });

        _exitButton.onClick.AddListener(ClickedExitButton);

        EnableControlsWindow(false);
    }

    private void ClickedStartGameButton()
    {
        SceneManager.LoadScene("Game");
    }

    private void ClickedExitButton()
    {
        Application.Quit();
    }

    private void EnableControlsWindow(bool enable)
    {
        _controls.SetActive(enable);
    }



}
