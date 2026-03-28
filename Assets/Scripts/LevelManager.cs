using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [Header("Managers")]
    public UIManager uiManager;
    public InventoryManager inventoryManager;

    [Header("Character Controller")]
    public Character character;

    [Header("Gameplay Objects")]
    public GameObject barriers;
    public Toggle toggle;
    public WallEye wallEye;
    public Door door;
    public GameObject inventoryItems;

    [Header("Prefabs")]
    public Inventory pumpkinPrefab;
    public Inventory lanternPrefab;


    private void Start()
    {
        //connect inventory and ui event
        inventoryManager.OnInventoryChanged.AddListener(uiManager.UpdateInventoryUI);
        inventoryManager.OnInventoryChanged.AddListener(LockDoorInventory);
        inventoryManager.OnInventorySpawned.AddListener(SpawnInventory);
        inventoryManager.OnInventoryFull.AddListener(uiManager.ShowInventoryFull);

        foreach (Transform child in inventoryItems.transform)
        {
            Inventory inventory = child.GetComponent<Inventory>();
            inventory.OnItemCollected.AddListener(inventoryManager.PickUpInventory);
        }

        //connect gameplay system 
        foreach (Transform child in barriers.transform)
        {
            Barrier barrier = child.GetComponent<Barrier>();
            toggle.OnToggle.AddListener(barrier.Move);
        }

        toggle.OnToggle.AddListener(wallEye.OpenClose);
        wallEye.OnEyeStateChanged.AddListener(LockDoorWallEye);

        character.OnInventoryShown.AddListener(uiManager.ShowInventory);
        character.OnItemDropped.AddListener(inventoryManager.DropInventory);
    }
    void LockDoorWallEye(WallEyeState eyeState)
    {
        if (eyeState == WallEyeState.Defeated)
        {
            door.SetLock(false);
        }
        else
        {
            door.SetLock(true);
        }
    }

    void LockDoorInventory()
    {
        if (inventoryManager.inventory[InventoryItem.Lantern] > 0)
        {
            door.SetLock(false);
        }
        else
        {
            door.SetLock(true);
        }
    }

    void SpawnInventory(InventoryItem item)
    {
        switch (item)
        {
            case InventoryItem.Pumpkin:
                PlaceInventory(pumpkinPrefab);
                break;
            case InventoryItem.Lantern:
                PlaceInventory(lanternPrefab);
                break;
        }
    }

    void PlaceInventory(Inventory inventoryPrefab)
    {
        Inventory inventory = Instantiate(inventoryPrefab);
        inventory.OnItemCollected.AddListener(inventoryManager.PickUpInventory);
        inventory.transform.position = character.transform.position + character.transform.forward;
    }


}