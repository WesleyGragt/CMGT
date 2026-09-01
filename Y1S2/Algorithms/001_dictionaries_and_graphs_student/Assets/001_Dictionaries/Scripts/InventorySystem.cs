using System.Collections.Generic;
using UnityEngine;

// InventorySystem class, complete all TODO's

public class InventorySystem : MonoBehaviour
{
    // TODO: Declare a dictionary to store inventory items and their counts...
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    // private ...

    public void AddItem(string itemName, bool isSpecial = false)
    {
        //Debug.Log("TODO: Implement AddItem logic (validate item, handle special items, update inventory count)");
        
        if (!isSpecial)
        {
            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName]++;
            }
            else
            {
                inventory[itemName] = 1;
            }
        } else inventory[itemName] = 1;
    }

    public void RemoveItem(string itemName)
    {
        //Debug.Log("TODO: Implement RemoveItem logic (validate item, decrease quantity, remove when zero)");

        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName]--;
            if (inventory[itemName] <= 0)
            {
                inventory.Remove(itemName);
            }
        }
        else
        {
            Debug.LogWarning("Item not found in inventory: " + itemName);
        }
    }

    public void DisplayInventory()
    {
        //Debug.Log("TODO: Implement DisplayInventory logic (iterate and print all inventory items)");

        foreach (var item in inventory)
        {
            Debug.Log(item.Key + ": " + item.Value);
        }
    }

    public bool HasItem(string itemName)
    {
        //Debug.Log("TODO: Implement HasItem logic (validate item and check existence in inventory)");
        if (inventory.ContainsKey(itemName))
        {
            return true;
        }
        return false;
    }
}
