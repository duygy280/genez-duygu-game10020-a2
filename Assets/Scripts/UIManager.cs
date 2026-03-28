using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public TextMeshProUGUI pumpkinsText;
    public TextMeshProUGUI lanternsText;

    public GameObject inventoryPanel;

    public TextMeshProUGUI activeInventoryText;
    public InventoryFull inventoryFull;

    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }
    public void UpdateInventoryUI()
    {
        int pumpkinsInventory = inventoryManager.inventory[InventoryItem.Pumpkin];
        pumpkinsText.text = $"Pumpkins: {pumpkinsInventory}";

        int lanternsInventory = inventoryManager.inventory[InventoryItem.Lantern];
        lanternsText.text = $"Lanterns: {lanternsInventory}";
    }
    public void ShowInventory(bool show)
    {
        inventoryPanel.SetActive(show);
    }
    public void SetPumpkinActive()
    {
        SetInventoryActive(InventoryItem.Pumpkin);
    }
    public void SetLanternsActive()
    {
        SetInventoryActive(InventoryItem.Lantern);
    }

    void SetInventoryActive(InventoryItem item)
    {
        inventoryManager.activeItem = item;
        activeInventoryText.text = $"Active Inventory: {item}";
    }

    public void ShowInventoryFull()
    {
        inventoryFull.ShowInventoryFull();
    }

    //this is triggered by an enemy hit event
    public void ShowHitFeedback()
    {
        Debug.Log("Enemy was hit!");
    }
}
