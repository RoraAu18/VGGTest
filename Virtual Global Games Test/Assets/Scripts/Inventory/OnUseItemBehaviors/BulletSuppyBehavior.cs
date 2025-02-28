using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSuppyBehavior : OnUseBehavior
{
    GunController gunController => GameManager.Instance.gunSpawner;
    public override void SpendItem(InventoryItemData data)
    {
        gunController.Recharge((int)data.powerAmt);
        base.SpendItem(data);
    }
}
