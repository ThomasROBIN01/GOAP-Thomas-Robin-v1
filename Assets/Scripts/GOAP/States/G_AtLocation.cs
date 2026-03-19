using UnityEngine;
using GOAP;

namespace GOAP
{
    public class G_AtLocation : G_State
    {
        // the value we are storing
        [SerializeField] LocationType value;

        #region Basic Controls

        public override void Construct(string name, object value)       // virtual type to be overwritten by inheriting classes
        {
            this.name = name;   // we didn't declare this name variable at the beginning, because the "object" type contain by default a name, which we are referencing here.
            SetValue(value);
        }

        public override void SetValue(object value)     // function to set the value externally
        {
            this.value = (LocationType)value;
        }

        public override object GetValue()
        {
            return value;
        }

        public override G_State Clone()
        {
            return An.AtLocation().WithName(name).WithLocationType(value);

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
            return false;
        }

        /// <summary>
        /// This function returns true if the two given conditions match their states, expected values, and comparisons
        /// </summary>
        /// <returns></returns>
        public override bool TestStateConditionMatch(G_Condition precondition, G_Condition effect)
        {
            return false;
        }

        /// <summary>
        /// Returns true if the state type has an implementation for the given comparison type
        /// </summary>
        /// <returns></returns>
        public override bool StateSupportsComparison(G_StateComparison comparison)
        {
            return false;
        }

        /// <summary>
        /// Test if the given value is of the same type as the value stored in this state and returns true if it is
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public override bool TestValueMatch(object testValue)
        {

            return false;               // if false, will return false
        }

        #endregion
    }
}
