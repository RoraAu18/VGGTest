using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUseBehavior : MonoBehaviour
{
    public InventoryItemType type;
    public virtual void SpendItem(InventoryItemData data)
    {
    }
}
