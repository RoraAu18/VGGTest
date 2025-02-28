using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class InventoryController : MonoBehaviour, iSpawnerUsers<InventoryItem>
{
    static InventoryController instance;
    public static InventoryController Instance => instance;
    GameManager gameManager => GameManager.Instance;
    public Spawner<InventoryItem> spawner;
    public List<InventoryItemData> currInventoryItems = new List<InventoryItemData>();
    [SerializeField] Button showHideButton;
    [SerializeField] Transform itemsContainer;
    InventoryItemData currDataToAdd;
    InventoryData inventoryData;
    public DataManager dataManager;
    [SerializeField] Animator animator;
    public List<OnUseBehavior> itemBehaviours = new List<OnUseBehavior>();
    public List<GunConfig> gunConfigs = new List<GunConfig>();
    public GunConfig currGunInUse;
    bool isVisible;
    public static string dataID = "inventoryData";
    private void Awake()
    {
        if(instance != null && instance != this) DestroyImmediate(this);
        instance = this;
    }
    public void Init()
    {
        spawner.Init(this);
        if(inventoryData == null)
        {
            inventoryData = new InventoryData();
            currInventoryItems = inventoryData.inFileData;
        }
        currGunInUse = gunConfigs[0];
        gameManager.onSavingData += OnSavingData;
        gameManager.onLoadingData += OnLoadingData;
        isVisible = false;
        showHideButton.onClick.AddListener(ShowHideInventory);
        DisplayDifferentGuns();
    }
    #region UI
    public void ShowHideInventory()
    {
        if (isVisible) animator.SetTrigger("hide");
        else animator.SetTrigger("show");
        isVisible = !isVisible;
    }
    #endregion
    public void OnSavingData()
    {
        inventoryData.inFileData = currInventoryItems;
        inventoryData.gunInUseIdx = gunConfigs.IndexOf(currGunInUse);
        dataManager.SaveData(dataID, inventoryData);
    }
    public void OnLoadingData()
    {
        inventoryData = dataManager.GetData<InventoryData>(dataID);
        if (inventoryData.inFileData.Count > 0)
        {
            for (int i = 0; i < inventoryData.inFileData.Count; i++)
            {
                SetItemInInventory(inventoryData.inFileData[i]);
            }
            currGunInUse = gunConfigs[inventoryData.gunInUseIdx];
        }
    }
    void DisplayDifferentGuns()
    {
        for (int i = 0; i < gunConfigs.Count; i++)
        {
            //SetItemInInventory();
        }
    }
    public void SetItemInInventory(InventoryItemData item)
    {
        currDataToAdd = item;
        spawner.SpawnNewItem();
        currInventoryItems.Add(item);
    }
    public void RemoveItem(InventoryItemData itemToRemove)
    {
        currInventoryItems.Remove(itemToRemove);
    }
    public void OnSpawnedCustomizable(InventoryItem newItem, Pool<InventoryItem> pool)
    {
        newItem.Init(currDataToAdd, GetOnUseBehaviour(currDataToAdd.itemType));
    }
    public OnUseBehavior GetOnUseBehaviour(InventoryItemType type)
    {
        for (int i = 0; i < itemBehaviours.Count; i++)
        {
            if (itemBehaviours[i].type == type) return itemBehaviours[i];
        }
        return itemBehaviours[0];
    }
}
public class InventoryData
{
    public List<InventoryItemData> inFileData = new List<InventoryItemData>();
    public int gunInUseIdx;
} 