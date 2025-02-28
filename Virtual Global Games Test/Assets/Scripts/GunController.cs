using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour, iSpawnerUsers<BaseBulletController>
{
    [SerializeField] Spawner<BaseBulletController> bulletSpawner;
    public int availBullets;
    public int initBullets;
    [SerializeField] GunConfig currGunConfig;
    [SerializeField] MeshRenderer gunMaterial;
    public void Init()
    {
        bulletSpawner.Init(this);
        availBullets = initBullets;
        SetBulletAmtText();
        currGunConfig = InventoryController.Instance.currGunInUse;
    }
    void SetBulletAmtText() => UIManager.Instance.SetBulletAmount(availBullets);
    public void EquipGun(GunConfig newConfig)
    {
        gunMaterial.material = newConfig.gunMat;
        currGunConfig = newConfig;
    }
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
