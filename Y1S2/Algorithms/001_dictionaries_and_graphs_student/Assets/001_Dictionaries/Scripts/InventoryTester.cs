using NaughtyAttributes;
using System;
using UnityEngine;

// InventoryTester class, no modifications needed.

public class InventoryTester : MonoBehaviour
{
    private InventorySystem inventory;

    public string item = "";
    public bool special = false;

    [InfoBox("Inventory", EInfoBoxType.Normal)]
    [SerializeField]
    [TextArea(10, 10)]
    private string contents = "";

    private void Awake()
    {
        // Initialize the inventory system
        inventory = GetComponent<InventorySystem>();

        // Receive debug.log callbacks
        Application.logMessageReceived += logHandler;
    }

    private void logHandler(string info, string stackTrace, LogType type)
    {
        contents += info + Environment.NewLine;
    }

    private void OnApplicationQuit()
    {
        Application.logMessageReceived -= logHandler;
    }

    [Button("Add item", EButtonEnableMode.Playmode)]
    private void AddItem()
    {
        ClearLog();
        inventory.AddItem(item, special);
    }

    [Button("Remove item", EButtonEnableMode.Playmode)]
    private void RemoveItem()
    {
        ClearLog();
        inventory.RemoveItem(item);
    }

    [Button("Has item", EButtonEnableMode.Playmode)]
    private void HasItem()
    {
        ClearLog();
        Debug.Log("Inventory contains " + item + ":" + inventory.HasItem(item));
    }

    [Button("Show inventory", EButtonEnableMode.Playmode)]
    private void ShowInventory()
    {
        ClearLog();
        inventory.DisplayInventory();
    }

    private void ClearLog()
    {
        contents = "";
    }

    /*
    void Start()
    {
        // Initialize the inventory system
        inventory = GetComponent<InventorySystem>();

        // Add some items
        inventory.AddItem("Health Potion");
        inventory.AddItem("Mana Potion");
        inventory.AddItem("Health Potion"); // Increase quantity
        inventory.AddItem("Golden Key", true); // Add a special item
        inventory.AddItem("Golden Key", true); // Attempt to add again (should fail)

        // Remove an item
        inventory.RemoveItem("Health Potion");

        // Display the inventory
        inventory.DisplayInventory();

        // Check if an item exists
        if (inventory.HasItem("Mana Potion"))
        {
            Debug.Log("Mana Potion is in the inventory!");
        }
    }
    */


}