using UnityEngine;
using GOAP;

namespace GOAP
{
    // [CreateAssetMenu(fileName = "G_State", menuName = "Scriptable Objects/G_State")]     // Removed because we don't want to it instantiable in the Editor
    public class G_State : ScriptableObject
    {
        // the value we are storing
        object value;   // object type can be of any type of variable

        #region Basic Controls

        public virtual void Construct(string name, object value)       // virtual type to be overwritten by inheriting classes
        {
            this.name = name;   // we didn't declare this name variable at the beginning, because the object type contain by default a name, which we are referencing here.
            SetValue(value);
        }

        public virtual void SetValue(object value)     // function to set the value externally
        {
            this.value = value;
        }

        public virtual object GetValue()
        {
            return value;
        }

        public virtual G_State Clone()
        {
            // G_State clone = new G_State();  // this would be ideal, but unfortunately, Unity doesn't support this, so we need to use the CreateInstance instead below
            G_State clone = ScriptableObject.CreateInstance<G_State>();
            clone.Construct(this.name, this.value);
            return clone;
        }

        #endregion


        #region Testing Controls

        /// <summary>
        /// Tests the given state against the expected value using the chosen comparison, returning true if the comaprison is correct and false if not
        /// </summary>
        /// <param name="state"></param>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public virtual bool TestState(G_State state, G_StateComparison comparison, object expectedValue)
        {
            Debug.LogWarning($"Base class G_State has no state testing implemented - returning false for {state.name}");
            // if we see that message, this means the planner is reading the base class version rather than any of the inheriting classes.
            // So the test hasn't been properly implemented in any of the inheriting classes
            // we want to make sure we overwrite that function and use the inheriting class

            return false;
        }

        /// <summary>
        /// This function returns true if the two given conditions match their states, expected values, and comparisons
        /// </summary>
        /// <returns></returns>
        public virtual bool TestStateConditionMatch(G_Condition precondition, G_Condition effect)
        {
            Debug.LogWarning($"Base class G_State has no condition comparisons implemented - returning false");
            return false;
        }

        /// <summary>
        /// Returns true if the state type has an implementation for the given comaprison type
        /// </summary>
        /// <returns></returns>
        public virtual bool StateSupportsComparison(G_StateComparison comparison)
        {
            Debug.LogWarning($"Base class G_State doesn't support any comparison - returning false");
            return false;
        }

        /// <summary>
        /// Test if the given value is of the same type as the value stored in this state and returns true if it is
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public virtual bool TestValueMatch(object testValue)
        {
            Debug.LogWarning($"Base class G_State doesn't support value matching - returning false");
            // return testValue is bool;   // if testValue is a boolean, this will return true.
            return false;               // if false, will return false
        }

        #endregion
    }
}
