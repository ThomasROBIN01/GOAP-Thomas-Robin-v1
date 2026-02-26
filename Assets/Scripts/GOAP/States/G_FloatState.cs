using NUnit.Framework.Internal;
using System;
using UnityEngine;

namespace GOAP
{
    [CreateAssetMenu(fileName = "New Float State", menuName = "GOAP/States/FloatState")]       // We've renamed the filename and chage the menuName to have something cleaner and tiny as a menu setup 
    public class G_FloatState : G_State      // Will inherit from G_State instead of Scriptable Object
    {
        float value;

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
            if (TestValueMatch(value))      // just to make sure value is a float type, otherwise we'll not be able to perform the operation: this.value = value. We can also use: "value is float", but we have the TestValueMatch function that checks this, so let's use it
            {
                this.value = (float)value;   // value is an "object" type which can basically be of any type. So we "force" its type to be a float by using "(float)"
            }
        }

        public override G_State Clone()
        {
            G_FloatState clone = CreateInstance<G_FloatState>();
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
            return testValue is float;       // will return true if testValue is a boolean, false if not
        }

        /// <summary>
        /// This returns true if the comparison type is either equal or not equal
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public override bool StateSupportsComparison(G_StateComparison comparison)
        {
            return comparison != G_StateComparison.not_equal;
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
            bool result = false;
            float stateFloat = (float)state.GetValue();
            float expectedFloat = (float)expectedValue;

            switch (comparison)
            {
                case G_StateComparison.equal:
                    result = (stateFloat == expectedFloat);     // the expression into parenthesis just returns true or false
                    break;
                case G_StateComparison.greater:
                    result = (stateFloat > expectedFloat);
                    break;
                case G_StateComparison.greaterOrEqual:
                    result = (stateFloat >= expectedFloat);
                    break;
                case G_StateComparison.lesser:
                    result = (stateFloat < expectedFloat);
                    break;
                case G_StateComparison.lesserOrEqual:
                    result = (stateFloat <= expectedFloat);
                    break;
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
            bool result = false;
            return result;
        }

        #endregion

        #region Conditions

        #endregion

    }
}
