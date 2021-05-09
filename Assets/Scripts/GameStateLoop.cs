using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateLoop : MonoBehaviour
{
    public static GameState CurrentGameState;
    public static float Elapsed;
    public float BackToMenuDelay;
    public float StartSpawninngDelay;
    [SerializeField] private PlayerData PlayerHealth;
    [SerializeField] private EventTriggerData SpawnerState;
    
    public enum GameState : int
    {
        OnMenu = 1,
        BeforeTheStorm,
        ItemShopCombat,
        GameOver,
        Restart
    }

    private void Awake()
    {
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
                if (Elapsed >= StartSpawninngDelay)
                {
                    SetGameState(GameState.ItemShopCombat);
                    SpawnerState.IsActive = true;
                }
                break;
            case GameState.ItemShopCombat:
                if (PlayerHealth.CurrentValue <= 0f)
                {
                    SetGameState(GameState.GameOver);
                }
                break;
            case GameState.GameOver:
                // show score / high score
                
                if (Elapsed >= BackToMenuDelay)
                {
                    SetGameState(GameState.OnMenu);
                }
                
                break;
            default:
                return;
        }
    }

    public static void SetGameState(GameState newState)
    {
        Elapsed = 0f;
        CurrentGameState = newState;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicStateParameter", (float)CurrentGameState);
    }
    
    public static void OnGameStarted()
    {
        CurrentGameState = GameState.BeforeTheStorm;
    }
}
