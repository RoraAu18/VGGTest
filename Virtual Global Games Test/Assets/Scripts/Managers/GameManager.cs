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
    [SerializeField] EnemiesSpawner enemiesSpawner;
    [SerializeField] TextMeshProUGUI wonLostLabel;
    [SerializeField] TextMeshProUGUI firstIntroLabel;
    [SerializeField] Button restartButton;
    public Action onSavingData;
    public Action onLoadingData;
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
        restartButton.onClick.AddListener(Init);
        Init();
    }
    void Init()
    {
        enemiesSpawner.RestartSpawner();
        gunSpawner.RestartSpawner();
        wonLostLabel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        healthController.Init();
        inventoryController.Init();
        gunSpawner.Init();
        player.Init();
        StartCoroutine(Intro());
        gameState = GameState.GameOn;
        onLoadingData?.Invoke();
    }
    IEnumerator Intro()
    {
        firstIntroLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        firstIntroLabel.gameObject.SetActive(false);
    }
    public void OnGameEnded(bool won)
    {
        if (won) wonLostLabel.text = "You Won";
        else wonLostLabel.text = "You Lost";
        wonLostLabel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameState = GameState.GameOff;
        onSavingData?.Invoke();
    }
}
public enum GameState
{
    GameOn,
    GameOff
}