using UnityEngine;

namespace GOAP
{
    public class G_Condition
    {
        #region Variables

        G_State state;  // the state being tested by the condition
        public G_State State
        {
            get { return state; }   // we will be able to read the value through this property but not able to set it
        }

        G_StateComparison comparison;   // the actual comparison for the condition
        public G_StateComparison Comparison
        {
            get { return comparison; }
        }

        object expectedValue;   // the value we will be comparing to the current value in the state
        public object ExpectedValue
        {
            get { return expectedValue; }
        }

        bool met = false;   // has the condition been met during planning?
        public bool Met
        {
            get { return met; }
        }

        #endregion
    }
}
