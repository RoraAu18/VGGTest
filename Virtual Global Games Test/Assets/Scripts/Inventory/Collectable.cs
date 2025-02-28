using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public InventoryController inventoryController => InventoryController.Instance;
    public InventoryItemData inventoryItemData;
    public virtual void OnCollected()
    {
        Debug.Log("Collected");
        inventoryController.SetItemInInventory(inventoryItemData);
        gameObject.SetActive(false);
    }
}
