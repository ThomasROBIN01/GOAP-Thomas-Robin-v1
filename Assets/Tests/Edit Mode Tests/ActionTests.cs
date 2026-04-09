using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class ActionTests
{
    [Test]
    public void ActionCloning()
    {
        
    }

    [Test]
    public void TestEffectAgainstPreconditions()
    {

    }

    public class SlicedBreadData
    {
        public GameObject go;
        public Inventory inventoryComponent;

        // States:
        G_AtLocation at_location;
        G_Inventory inventory;
        G_BoolState is_able;

        // Actions:
        // precondition action
        public G_Action go_to_kitchen;
        // effect action
        public G_Action slice_bread;

        // Location
        public LocationType kitchen = A.LocationType("kitchen");

        // Items
        public Item bread = An.Item("bread").IsStackable(true);
        public Item bread_knife = An.Item("bread_knife").IsStackable(false);
        public Item sliced_bread = An.Item("sliced_bread").IsStackable(true);



        public SlicedBreadData()
        {
            // Inventory data
            go = new GameObject();
            inventoryComponent =  go.AddComponent<Inventory>();

            inventoryComponent.AddToInventory(new ItemStack(bread, 1));
            inventoryComponent.AddToInventory(new ItemStack(bread_knife, 1));

            // states setup
            is_able = A.BoolState().WithName("is_able").WithValue(true);
            inventory = An.InventoryState("inventory").WithInventory(inventoryComponent);
            at_location = An.AtLocation().WithName("at_location");

            // go_to_kitchen = An.Action("go_to_kitchen").WithEffect(A.Condition().WithState(at_location).WithComparison(G_StateComparison.equal).WithExpectedValue(kitchen));
            go_to_kitchen = An.Action("go_to_kitchen").WithEffect(A.Condition().WithState(at_location).WithExpectedValue(kitchen));     // WithComparison is not really needed

            slice_bread = An.Action("slice_bread")
                .WithPrecondition(A.Condition().WithState(inventory).WithExpectedValue(ItemStack.EmptyStack(bread_knife)).WithComparison(G_StateComparison.greater))
                .WithPrecondition(A.Condition().WithState(inventory).WithComparison(G_StateComparison.greater).WithExpectedValue(ItemStack.EmptyStack(bread)))
                .WithPrecondition(A.Condition().WithState(at_location).WithExpectedValue(kitchen))
                .WithPrecondition(A.Condition().WithState(is_able).WithExpectedValue(true))


                .WithEffect(A.Condition().WithState(inventory).WithComparison(G_StateComparison.greater).WithExpectedValue(ItemStack.EmptyStack(sliced_bread)));

        }
    }
}
