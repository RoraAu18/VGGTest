using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class InventoryItem : MonoBehaviour, IPointerUpHandler
{
    InventoryController inventoryController => InventoryController.Instance;
    public InventoryItemData data;
    public TextMeshProUGUI itemName;
    [SerializeField] bool isReusable;
    public OnUseBehavior onUseBehaviour;
    public void Init(InventoryItemData _data, OnUseBehavior _onUseBehaviour)
    {
        data = _data;
        onUseBehaviour = _onUseBehaviour;
        itemName.text = _data.itemName;
    }
    void Update()
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pressing ", this);
        if (EventSystem.current.currentSelectedGameObject == this)
        {
            Debug.Log("pressing ", this);
            onUseBehaviour.SpendItem(data);
        }
    }
}
[Serializable]
public class InventoryItemData
{
    public string itemName;
    public int itemCount;
    public float powerAmt;
    public InventoryItemType itemType;
}
public enum InventoryItemType
{
    Gun = 1,
    Medicine = 2,
    Bullet = 3
}