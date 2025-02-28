using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour, iSpawnerUsers<BaseBulletController>
{
    [SerializeField] Spawner<BaseBulletController> bulletSpawner;
    public int availBullets;
    public int initBullets;
    [SerializeField] GunConfig currGunConfig;
    public TextMeshProUGUI availBulletsLabel;
    public void Init()
    {
        bulletSpawner.Init(this);
        availBullets = initBullets;
        SetBulletAmtText();
        currGunConfig = InventoryController.Instance.currGunInUse;
    }
    void SetBulletAmtText()
    {
        availBulletsLabel.text = "Bullets: " + availBullets.ToString();
    }
    public void EquipGun(GunConfig newConfig) => currGunConfig = newConfig;
    public void Recharge(int extraBullets)
    {
        availBullets += extraBullets;
        SetBulletAmtText();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && availBullets > 0)
        {
            SpendBullet();
        }
    }
    void SpendBullet()
    {
        bulletSpawner.SpawnNewItem();
        availBullets--;
        SetBulletAmtText();
    }
    public void OnSpawnedCustomizable(BaseBulletController newItem, Pool<BaseBulletController> pool)
    {
        newItem.Init(this, pool, currGunConfig);
    }
    public void RestartSpawner()
    {
        bulletSpawner.pool.RecycleAll();
    }
}
