using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateLoop : MonoBehaviour
{
    public GameState CurrentGameState;
    public float Elapsed;
    public float BackToMenuDelay;
    public float StartCombatDelay;
    public float RestartGameDelay;
    [SerializeField] private PlayerData PlayerHealth;
    [SerializeField] private EventTriggerData SpawnerState;
    
    public enum GameState : int
    {
        OnMenu = 0,
        BeforeTheStorm,
        ItemShopCombat,
        GameOver,
        Restart
    }

    private void Awake()
    {
        StartCombatDelay = 4;
        CurrentGameState = GameState.OnMenu;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicStateParameter", (float)CurrentGameState);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Elapsed += Time.deltaTime;
        switch (CurrentGameState)
        {
            case GameState.OnMenu:
                // if gamemenu button pressed....
                SetGameState(GameState.BeforeTheStorm);
                break;
            case GameState.BeforeTheStorm:
                if (Elapsed >= StartCombatDelay)
                {
                    SetGameState(GameState.ItemShopCombat);
                    SpawnerState.IsActive = true;
                }
                break;
            case GameState.ItemShopCombat:
                if (PlayerHealth.CurrentValue <= 0)
                {
                    SetGameState(GameState.GameOver);
                }
                break;
            case GameState.GameOver:
                // show score / high score
                SpawnerState.IsActive = false;
                if (Elapsed >= BackToMenuDelay)
                {
                    SetGameState(GameState.OnMenu);
                }
                break;
            case GameState.Restart:
                if (Elapsed >= RestartGameDelay)
                {
                    PlayerHealth.ChangeValue(PlayerHealth.StartAmount - PlayerHealth.CurrentValue);
                    SceneManager.LoadScene("Game");
                }
                break;
            default:
                return;
        }
    }

    public void SetGameState(GameState newState)
    {
        Elapsed = 0f;
        CurrentGameState = newState;
        Debug.Log(CurrentGameState);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicStateParameter", (float)CurrentGameState);
    }
    
    public void OnGameStarted()
    {
        CurrentGameState = GameState.BeforeTheStorm;
    }
}
