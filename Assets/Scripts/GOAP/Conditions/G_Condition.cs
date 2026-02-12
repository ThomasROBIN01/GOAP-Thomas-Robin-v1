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

        // Constructer
        public G_Condition (G_State state, object expectedValue, G_StateComparison comparison = G_StateComparison.equal, bool met = false)
        {
            this.state = state;
            this.expectedValue = expectedValue;
            this.comparison = comparison;            
            this.met = met;
        }

        #region Functions

        /// <summary>
        /// Returns true if this condition mataches the effect condition parameter
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public bool CompareConditionToEffect (G_Condition effect)
        {
            if(state.TestValueMatch(effect.state.GetValue()))
            {
                return state.TestStateConditionMatch(this, effect);
            }
            else
            {
                return false; 
            }

        }

        /// <summary>
        /// Returns true if the condition defined in this condition instance is actually correct
        /// </summary>
        /// <returns></returns>
        public bool DoesStateMeetCondition()        // for the stored state
        {
            return state.TestState(state, comparison, expectedValue);
        }

        /// <summary>
        /// Returns true if the condition defined in this condition instance is actually correct when applied to an external state
        /// </summary>
        /// <param name="stateToTest"></param>
        /// <returns></returns>
        public bool DoesStateMeetCondition(G_State stateToTest)     // for external state
        {
            bool success = false;

            if (state.TestValueMatch(stateToTest.GetValue()))
            {
                success = state.TestState(stateToTest, comparison, expectedValue);
            }
            else
            {
                Debug.LogWarning("Value type of state to test did not matach internal state");
            }

            return success;
        }

        /// <summary>
        /// Returns true if the state to compare has the same name as the state in this condition
        /// </summary>
        /// <param name="stateToCompare"></param>
        /// <returns></returns>
        public bool IsStateTheConditionState (G_State stateToCompare)
        {
            return stateToCompare.name == state.name;
        }

        /// <summary>
        /// Sets the variable met to true. Used during planning
        /// </summary>
        public void Meet()
        {
            met = true;
        }


        public static G_Condition Clone (G_Condition conditionToClone)
        {
            return new G_Condition(conditionToClone.state, conditionToClone.expectedValue, conditionToClone.comparison, conditionToClone.met);
        }

        #endregion
    }
}
