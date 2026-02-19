using NUnit.Framework.Internal;
using System;
using UnityEngine;

namespace GOAP
{
    [CreateAssetMenu(fileName = "New Bool State", menuName = "GOAP/States/BoolState")]       // We've renamed the filename and chage the menuName to have something cleaner and tiny as a menu setup 
    public class G_BoolState : G_State      // Will inherit from G_State instead of Scriptable Object
    {
        bool value;

        #region Basic Controls

        public override void Construct(string name, object value)
        {
            this.name = name;
            SetValue(value);
        }

        public override object GetValue()   // overriding the function inherited from G_State
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (TestValueMatch(value))      // just to make sure value is a bool type, otherwise we'll not be able to perform the operation: this.value = value. We can also use: "value is bool", but we have function that check this, so let's use it
            {
                this.value = (bool)value;   // value is an "object" type which can basically be of any type. So we "force" its type to be bool by using (bool)
            }
        }

        public override G_State Clone()
        {
            G_BoolState clone = CreateInstance<G_BoolState>();
            clone.Construct(this.name, this.value);
            return clone;
        }

        #endregion


        #region Testing Controls

        /// <summary>
        /// Returns true if the value entered in a bool
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public override bool TestValueMatch(object testValue)
        {
            return testValue is bool;       // will return true if testValue is a boolean, false if not
        }

        /// <summary>
        /// This returns true if the comparison type is either equal or not equal
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public override bool StateSupportsComparison(G_StateComparison comparison)
        {
            return comparison == G_StateComparison.equal || comparison == G_StateComparison.not_equal;
        }

        /// <summary>
        /// Test the given state's value against the expected value using the given comparison and return true if the result matches expectations
        /// </summary>
        /// <param name="state"></param>
        /// <param name="comparison"></param>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public override bool TestState(G_State state, G_StateComparison comparison, object expectedValue)
        {
            bool stateValue = (bool)state.GetValue();       // using (bool) just to make sure it is really a bool type
            bool testValue = (bool)expectedValue;
            bool result = false;

            if (comparison == G_StateComparison.equal)
            {
                result = stateValue == testValue;       // boolean expression which will set result to true if stateValue == testValue
            }
            else if (comparison == G_StateComparison.not_equal)
            {
                result = stateValue != testValue;       // boolean expression which will set result to true if stateValue is different than testValue, and false if they're the same
            }
            else
            {
                Debug.LogWarning($"Bool state does not support {comparison} comparison");
            }

                return result;            
        }

        /// <summary>
        /// Return true if both conditions are referring to the same state, have the same comparison, and expected value
        /// </summary>
        /// <param name="precondition"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public override bool TestStateConditionMatch(G_Condition precondition, G_Condition effect)
        {
            bool preValue = (bool)precondition.State.GetValue();
            bool effectValue = (bool)effect.State.GetValue();
            bool preComparedValid = StateSupportsComparison(precondition.Comparison);       // this will check and ensure that the comparison is valid for the state 
            bool effectComparedValid = StateSupportsComparison(effect.Comparison);          // this will check and ensure that the comparison is valid for the state 

            bool result = false;

            // avoiding errors due to invalid comparison
            if(!preComparedValid || !effectComparedValid)       // if either of preCompared or effectCompared is invalid
            {
                Debug.LogWarning($"Invalid comparison found when comparing one or more conditions for bool state");
            }

            //else if (precondition.Comparison == G_StateComparison.equal && effect.Comparison == G_StateComparison.equal && preValue == effectValue)
            //{
            //    result = true;
            //}
            //else if (precondition.Comparison == G_StateComparison.not_equal && effect.Comparison == G_StateComparison.not_equal && preValue != effectValue)
            //{
            //    result = true;
            //}

            //else if (precondition.Comparison == G_StateComparison.equal && effect.Comparison == G_StateComparison.equal && preValue == effectValue
            //    || precondition.Comparison == G_StateComparison.not_equal && effect.Comparison == G_StateComparison.not_equal && preValue != effectValue)   // same as the 2 previous else if but combined in one
            //{
            //    result = true;
            //}

            else if (CompareConditions(precondition.Comparison, effect.Comparison, preValue, effectValue))       // we even cleaned further this if statement compared to the previous commented ones, by using the CompareConditions function below
            {
                result = true;
            }

                return result;
        }

        #endregion

        #region Conditions

        bool CompareConditions (G_StateComparison preconditionCompare, G_StateComparison effectCompare, bool preValue, bool effectValue)
        {
            return CompareEqual(preconditionCompare, effectCompare, preValue, effectValue) || CompareNotEqual(preconditionCompare, effectCompare, preValue, effectValue);
        }

        bool CompareEqual(G_StateComparison preconditionCompare, G_StateComparison effectCompare, bool preValue, bool effectValue)
        {
            return preconditionCompare == G_StateComparison.equal 
                && effectCompare == G_StateComparison.equal 
                && preValue == effectValue;
        }

        bool CompareNotEqual(G_StateComparison preconditionCompare, G_StateComparison effectCompare, bool preValue, bool effectValue)
        {
            return preconditionCompare == G_StateComparison.not_equal 
                && effectCompare == G_StateComparison.not_equal 
                && preValue != effectValue;
        }

        #endregion

    }
}
