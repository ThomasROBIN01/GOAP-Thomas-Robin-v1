using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class InventoryStateTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void InventoryCloneTest()
    {
        
    }

    [TestCase(true, true, true, true, 1, true, TestName = "All valid")]
    [TestCase(false, true, true, true, 1, false, TestName = "State invalid")]
    [TestCase(true, false, true, true, 1, false, TestName = "Expected Value invalid")]
    [TestCase(true, true, false, true, 1, false, TestName = "Item invalid")]
    [TestCase(true, true, true, false, 0, true, TestName = "Item invalid and Expected Value 0")]
    public void InventoryTestState(bool stateIsValid, bool expectedIsValid, bool itemIsValid, bool itemInInventory, int expectedQuantity, bool expectedResult)
    {
        GameObject go = new GameObject();       // the reason to do make a GameObject is because we need to add an inventory component
        Inventory inventory = go.AddComponent<Inventory>();

        Item testItem = null;

        G_Inventory testState = An.InventoryState("test");

        ItemStack expectedValue = null;

        if (stateIsValid)
        {
            testState = An.InventoryState("test").WithInventory(inventory);
        }
        if (itemIsValid)
        {
            testItem = An.Item("axe").IsStackable(false);
        }
        if (itemInInventory)
        {
            inventory.AddToInventory(new ItemStack(testItem, expectedQuantity));
        }
        if (expectedIsValid)
        {
            expectedValue = new ItemStack(testItem, expectedQuantity);
        }

        bool result = testState.TestState(testState, G_StateComparison.equal, expectedValue);

        Assert.AreEqual(expectedResult, result);
    }

    [TestCase (true, G_StateComparison.equal, 1, G_StateComparison.equal, 1, true, TestName = "Both equal to 1 item")]
    [TestCase(true, G_StateComparison.equal, 1, G_StateComparison.equal, 0, false, TestName = "Not equal to same quantity")]
    [TestCase(true, G_StateComparison.equal, 1, G_StateComparison.equal, 1, true, TestName = "Different Items")]
    [TestCase(true, G_StateComparison.equal, 1, G_StateComparison.greater, 0, true, TestName = "Equal 1 vs Greater 0")]
    public void InventoryConditionCompare(bool useSameItem, G_StateComparison preCompare, int preQuantity, G_StateComparison effectCompare, int effectQuantity, bool expectedResult)
    {
        GameObject go = new GameObject();       // the reason to do make a GameObject is because we need to add an inventory component
        Inventory inventory = go.AddComponent<Inventory>();
        Item axe = An.Item("axe").IsStackable(false);
        Item wood = An.Item("axe").IsStackable(true);
        G_Inventory testState = An.InventoryState("test").WithInventory(inventory);

        ItemStack preExpectedValue = new ItemStack(axe, 1);
        ItemStack effectExpectedValue = useSameItem? new ItemStack(axe, 1): new ItemStack(wood, 1);


        G_Condition precondition = A.Condition().WithState(testState).WithComparison(preCompare).WithExpectedValue (effectExpectedValue);
        G_Condition effect = A.Condition().WithState(testState).WithComparison(effectCompare).WithExpectedValue(effectExpectedValue);

        bool result = precondition.CompareConditionToEffect(effect);

        Assert.AreEqual(expectedResult, result);
    }
}
