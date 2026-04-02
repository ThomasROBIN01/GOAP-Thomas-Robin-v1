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

    [TestCase(true, true, true, 1, true, TestName = "All valid")]
    [TestCase(false, true, true, 1, false, TestName = "State invalid")]
    [TestCase(true, false, true, 1, false, TestName = "Expected Value invalid")]
    [TestCase(true, true, false, 1, false, TestName = "Item invalid")]
    [TestCase(true, true, false, 0, true, TestName = "Item invalid and Expected Value 0")]
    public void InventoryCanTestState(bool stateIsValid, bool expectedIsValid, bool itemIsValid, int expectedQuantity, bool expectedResult)
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
        if (expectedIsValid)
        {
            expectedValue = new ItemStack(testItem, expectedQuantity);
        }
        if (itemIsValid)
        {
            testItem = An.Item("axe").IsStackable(false);
            inventory.AddToInventory(new ItemStack(testItem, 1));
        }

        bool result = testState.TestState(testState, G_StateComparison.equal, expectedValue);

        Assert.AreEqual(expectedResult, result);
    }

    [TestCase (TestName = "Perfect Pass")]
    public void InventoryTestState()
    {
        // Use the Assert class to test conditions
    }

    [Test]
    public void InventoryConditionCompare()
    {
        // Use the Assert class to test conditions
    }
}
