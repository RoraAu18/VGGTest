using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedsBehavior : OnUseBehavior
{
    HealthController healthController => HealthController.Instance;
    public override void SpendItem(InventoryItemData data)
    {
        healthController.AddHealth(data.powerAmt);
        base.SpendItem(data);
    }
}
