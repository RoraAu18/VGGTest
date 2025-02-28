using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance => instance;
    [SerializeField] TextMeshProUGUI enemiesCountLabel;
    [SerializeField] TextMeshProUGUI wonLostLabel;
    [SerializeField] TextMeshProUGUI firstIntroLabel;
    [SerializeField] public Button restartButton;
    public Button saveButton;
    [SerializeField] TextMeshProUGUI availBulletsLabel;
    private void Awake()
    {
        if (instance != null && instance != this) DestroyImmediate(this);
        instance = this;
    }
    public void Init()
    {
        wonLostLabel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        StartCoroutine(Intro());
    }
    IEnumerator Intro()
    {
        firstIntroLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        firstIntroLabel.gameObject.SetActive(false);
    }
    public void GameEnd(bool won)
    {
        if (won) wonLostLabel.text = "You Won";
        else wonLostLabel.text = "You Lost";
        wonLostLabel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void SetBulletAmount(int bulletAmt) => availBulletsLabel.text = "Bullets: " + bulletAmt.ToString();   
    public void SetEnemiesAmount(int enemiesAmt) => enemiesCountLabel.text = "Enemies left: " + enemiesAmt.ToString();
}
