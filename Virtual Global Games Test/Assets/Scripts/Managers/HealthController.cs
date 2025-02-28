using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    static HealthController instance;
    public static HealthController Instance => instance;
    [SerializeField] Slider healthSlider;
    [SerializeField] float healthCounter;
    [SerializeField] float totalHealth = 1;
    [SerializeField] float reduceBy = 0.1f;
    private void Awake()
    {
        if (instance != null && instance != this) DestroyImmediate(this);
        instance = this;
    }
    public void Init()
    {
        healthCounter = totalHealth;
        healthSlider.value = healthCounter;
    }
    public void AddHealth(float toAdd)
    {
        healthCounter += toAdd;
        healthSlider.value = healthCounter;
        HasLifeLeft();
    }
    public void ReduceHealth(float toRemove)
    {
        healthCounter -= toRemove;
        healthSlider.value = healthCounter;
        HasLifeLeft();
    }
    void HasLifeLeft()
    {
        if (healthCounter <= 0) GameManager.Instance.OnGameEnded(won: false); 
    }
}
