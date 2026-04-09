using GOAP;
using System;
using UnityEngine;

namespace GOAP
{
    public class G_Inventory : G_State
    {
        // the value we are storing
        Inventory value;

        #region Basic Controls

        public override void Construct(string name, object value)       // virtual type to be overwritten by inheriting classes
        {
            this.name = name;   // we didn't declare this name variable at the beginning, because the "object" type contain by default a name, which we are referencing here.
            SetValue(value);
        }

        public override void SetValue(object value)     // function to set the value externally
        {
            this.value = value as Inventory;
        }

        public override object GetValue()
        {
            return value;
        }

        public override G_State Clone()
        {
            G_Inventory clone = An.InventoryState(name).WithInventory(value);
            return clone;
        }

        #endregion


        #region Testing Controls

        /// <summary>
        /// Tests the given state against the expected value using the chosen comparison, returning true if the comparison is correct and false if not
        /// </summary>
        /// <param name="state"></param>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public override bool TestState(G_State state, G_StateComparison comparison, object expectedValue)
        {
            bool success = false;

            object stateValue = state.GetValue();

            if (CanTestState(stateValue, expectedValue))       // Essentially if the entered item is stacked with a null item it fails
            {
                success = TestInventoryState(stateValue, comparison, expectedValue);
            }

            return success;
        }

        bool TestInventoryState (object stateValue, G_StateComparison comparison, object expectedValue)
        {
            bool success = false;
            ItemStack expectedStack = expectedValue as ItemStack;
            Inventory testInventory = stateValue as Inventory;

            ItemStack inventoryStack = testInventory.FindInInventory(expectedStack.item);

            if (inventoryStack != null)
            {
                success = G_NumberConditionComparer.TestValues(inventoryStack.quantity, comparison, expectedStack.quantity);
            }
            else if (NullStackIsEqualToZero(inventoryStack, comparison, expectedStack))
            {
                success = true;
            }
            return success;
        }

        public override bool TestStateConditionMatch(G_Condition precondition, G_Condition effect)
        {
            bool success = false;
            ItemStack preExpectedStack = precondition.ExpectedValue as ItemStack;
            ItemStack effectExpectedStack = effect.ExpectedValue as ItemStack;

            if (CanCompareConditions(precondition, effect, preExpectedStack, effectExpectedStack))
            {
                success = G_NumberConditionComparer.CompareNumberCondition(preExpectedStack.quantity, precondition.Comparison, effectExpectedStack.quantity, effect.Comparison);
            }

            return success;
        }

        public override bool StateSupportsComparison(G_StateComparison comparison)
        {
            return comparison == G_StateComparison.equal
                || comparison == G_StateComparison.greater
                || comparison == G_StateComparison.lesser
                || comparison == G_StateComparison.greaterOrEqual
                || comparison == G_StateComparison.lesserOrEqual;
        }


        public override bool TestValueMatch(object testValue)
        {
            return testValue != null && (testValue is ItemStack || testValue is Inventory);      // so long as one of these in bracket is true, the expression will return true
        }
        #endregion

        #region Conditions

        bool CanTestState (object stateValue, object expectedValue)
        {
            return TestValueMatch(stateValue) && TestValueMatch(expectedValue) && (expectedValue as ItemStack).item != null;
        }

        bool NullStackIsEqualToZero(ItemStack inventoryStack, G_StateComparison comparison, ItemStack expectedStack)
        {
            return inventoryStack == null && comparison == G_StateComparison.equal && expectedStack.quantity == 0;
        }

        bool CanCompareConditions(G_Condition precondition, G_Condition effect, ItemStack preExpectedStack, ItemStack effectExpectedStack)
        {
            return precondition.IsStateTheConditionState(effect.State)       // test if the state matches first, there's no point to move forward and test the rest if it's not the case
                && TestValueMatch(precondition.State.GetValue())
                && TestValueMatch(effect.State.GetValue())
                && TestValueMatch(precondition.ExpectedValue)
                && TestValueMatch(effect.ExpectedValue)
                && preExpectedStack.item != null                            // no point to do it if the data is not right
                && effectExpectedStack.item != null                        // no point to do it if the data is not right
                && preExpectedStack.item == effectExpectedStack.item;
        }

        #endregion
    }
}
