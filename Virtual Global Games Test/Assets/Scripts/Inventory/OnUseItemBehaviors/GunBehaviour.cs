using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : OnUseBehavior
{
    [SerializeField] GunConfig gunType;
    GunController gunController => GameManager.Instance.gunSpawner;
    public override void SpendItem(InventoryItemData data)
    {
        gunController.EquipGun(gunType);
        base.SpendItem(data);
    }
}
