using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;
    public GameState gameState;
    public Transform startLine;
    public Transform finishLine;
    public GunController gunSpawner;
    public int enemiesCount;
    public int totalEnemiesPerLevel;
    public int enemiesSpawned;
    [SerializeField] EnemiesSpawner enemiesSpawner;
    public AudioSource source;
    public AudioClip clip;
    public bool aimingToShoot;

    public Action onSavingData;
    public Action onLoadingData;
    UIManager uiManager=> UIManager.Instance;
    HealthController healthController => HealthController.Instance;
    InventoryController inventoryController => InventoryController.Instance;
    public FPSPlayer player;
    private void Awake()
    {
        if(instance != null && instance != this) DestroyImmediate(this);
        instance = this;
    }
    private void Start()
    {
        uiManager.restartButton.onClick.AddListener(Init);
        uiManager.saveButton.onClick.AddListener(OnSaving);
        Init();
    }
    void Init()
    {
        enemiesSpawner.RestartSpawner();
        gunSpawner.RestartSpawner();
        uiManager.Init();
        healthController.Init();
        inventoryController.Init();
        gunSpawner.Init();
        player.Init();
        gameState = GameState.GameOn;
        onLoadingData?.Invoke();
        enemiesCount = 0;
        enemiesSpawned = 0;
        source.clip = clip;
        source.time = 0;
        source.Play();
    }
    public void OnSaving() => onSavingData?.Invoke();
    
    public void RemoveEnemy()
    { 
        enemiesCount--;
        uiManager.SetEnemiesAmount(enemiesCount);
        if (enemiesCount <= 0) OnGameEnded(won: true);
    }
    public void OnGameEnded(bool won)
    {
        source.Stop();
        player.ResetOnRestart();
        uiManager.GameEnd(won);
        gameState = GameState.GameOff;
        OnSaving();
    }
}
public enum GameState
{
    GameOn,
    GameOff
}