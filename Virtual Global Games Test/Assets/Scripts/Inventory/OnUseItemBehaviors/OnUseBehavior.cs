using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUseBehavior : MonoBehaviour
{
    public InventoryItemType type;
    public bool isReusable;
    public virtual void SpendItem(InventoryItemData data)
    {

    }
}
