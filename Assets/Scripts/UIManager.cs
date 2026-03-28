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

    //for health UI
    public TextMeshProUGUI healthText;

    //gameover UI
    public GameObject gameOverText;
    //restart button
    public GameObject restartButton;

    private void Awake()
    {
        inventoryPanel.SetActive(false);

        //hide game over screen at start
        if (gameOverText != null)
            gameOverText.SetActive(false);
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
    public void ShowHitFeedback()
    {
        Debug.Log("Enemy was hit!");
    }

    //update player health UI
    public void UpdateHealthUI(int health)
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }

    //show Gameover screen
    public void ShowGameOver()
    {
        if (gameOverText != null)
            gameOverText.SetActive(true);
        if(restartButton !=null)
            restartButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}