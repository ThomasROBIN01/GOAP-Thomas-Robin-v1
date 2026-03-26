using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemStack> inventory = new List<ItemStack>();


    // public List<InventorySlot> inventory = new List<InventorySlot>();   // list of inventory slots instead of item stack, to manage it a particular way.

    // This is if we wanted to have an expanded item stacks
    // public int slotCount = 10;

    // This is if we wanted to have an expanded item stacks
    //private void Start()
    //{
    //    // do a for loop to the number of slotCount, if we want to make the inventory more expended system
    //}

    /// <summary>
    /// For adding items to the Inventory. If it finds the item type in the inventory, it will add to the stack of that item
    /// If it doesn't it will start a new stack
    /// </summary>
    /// <param name="stack"></param>
    public void AddToInventory (ItemStack stack)
    {
        if (stack.item.stackable)
        {
            StackItem(stack);
        }
        else
        {
            inventory.Add(new ItemStack(stack));
        }
    }

    void StackItem (ItemStack stack)
    {
        ItemStack existingStack = FindInInventory(stack.item);

        if (existingStack != null)  // stack item
        {
            existingStack.quantity += stack.quantity;
        }
        else  // add new stack
        {
            inventory.Add(new ItemStack(stack));
        }
    }

    /// <summary>
    /// Returns a stack of the given item in the inventory if it is in there, otherwise returns null
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public ItemStack FindInInventory (Item item)
    {
        ItemStack foundStack = null;

        if (item != null)
        {

            foundStack = inventory.Find((stack) => stack.item == item);      // we call each item in the inventory a stack (stack) which is the parameter, on the left of the =>; and the condition on the right.
                                                                             // So each time we assess an ItemStack in the inventory, this function should return true if stack.item == item.
                                                                             // So if we got stack in the same item, this will return true, otherwise it will find nothing and return null
        }
        return foundStack;
    }

}

/// <summary>
/// Data type for item slots in inventories     // This is if we wanted to have an expanded item stacks
/// </summary>
public class InventorySlot
{
    ItemStack stack = new ItemStack();
}
