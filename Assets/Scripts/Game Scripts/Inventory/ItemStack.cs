using UnityEngine;

[System.Serializable]   // This is just a tag for custom editable stuff, or to see the inventory item in the Unity Editor 

public class ItemStack
{
    public Item item;

    public int quantity = 0;

    public ItemStack()
    {

    }

    public ItemStack(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    /// <summary>
    /// Use this overload to duplicate a stack
    /// </summary>
    /// <param name="stack"></param>
    public ItemStack (ItemStack stack)
    {
        this.item = stack.item;
        this.quantity = stack.quantity;
        
        //ItemStack.EmptyStack(item);
        //new ItemStack(item, 0);
        // Or we can use the EmptyStack method below instead
    }

    public static ItemStack EmptyStack(Item item)   // this could have been in the previous constructor ItemStack (ItemStack stack), but it's visually easier to differentiate an Empty Stack like this 
    {
        return new ItemStack(item, 0);
    }
}
