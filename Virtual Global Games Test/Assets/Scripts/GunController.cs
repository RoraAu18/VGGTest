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
    public float rotSpeed;
    Vector3 defaultPos;
    public void Init()
    {
        bulletSpawner.Init(this);
        availBullets = initBullets;
        SetBulletAmtText();
        defaultPos = transform.eulerAngles;
        currGunConfig = InventoryController.Instance.currGunInUse;
    }
    void AimToShoot()
    {
        GameManager.Instance.aimingToShoot = true;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.eulerAngles += Vector3.up * mouseX * rotSpeed * Time.deltaTime;
    }
    void HeldUp()
    {
        GameManager.Instance.aimingToShoot = false;
        transform.localEulerAngles = defaultPos;
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
        if (Input.GetKeyDown(KeyCode.LeftControl)) AimToShoot();
        if (Input.GetKeyUp(KeyCode.LeftControl)) HeldUp();
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
