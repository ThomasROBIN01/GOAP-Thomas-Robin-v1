using UnityEngine;

namespace GOAP
{
    public static class G_NumberConditionComparer       // static class means that class cannot be instantiated, instead the class is called directly.
                                                        // Nothing in this class is stored and nothing needs to persist => static. As there is no object created (instances) => better performance
    {
        #region TTestValues

        public static bool TestValues (int stateValue, G_StateComparison comparison, int expectedValue)
        {
            bool result = false;

            switch (comparison)
            {
                case G_StateComparison.equal:
                    result = (stateValue == expectedValue);     // the expression into parenthesis just returns true or false
                    break;
                case G_StateComparison.greater:
                    result = (stateValue > expectedValue);
                    break;
                case G_StateComparison.greaterOrEqual:
                    result = (stateValue >= expectedValue);
                    break;
                case G_StateComparison.lesser:
                    result = (stateValue < expectedValue);
                    break;
                case G_StateComparison.lesserOrEqual:
                    result = (stateValue <= expectedValue);
                    break;
            }
            return result;
        }

        public static bool TestValues(float stateValue, G_StateComparison comparison, float expectedValue)
        {
            bool result = false;

            switch (comparison)
            {
                case G_StateComparison.equal:
                    result = (stateValue == expectedValue);     // the expression into parenthesis just returns true or false
                    break;
                case G_StateComparison.greater:
                    result = (stateValue > expectedValue);
                    break;
                case G_StateComparison.greaterOrEqual:
                    result = (stateValue >= expectedValue);
                    break;
                case G_StateComparison.lesser:
                    result = (stateValue < expectedValue);
                    break;
                case G_StateComparison.lesserOrEqual:
                    result = (stateValue <= expectedValue);
                    break;
            }
            return result;
        }

        #endregion


        #region Compare Conditions
        public static bool CompareNumberCondition(float prevalue, G_StateComparison preCompare, float effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch(preCompare)
            {
                case G_StateComparison.equal:
                    success = TestEqual(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.greater:
                    success = TestGreater(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.greaterOrEqual:
                    success = TestGreaterOrEqual(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.lesser:
                    success = TestLesser(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.lesserOrEqual:
                    success = TestLesserOrEqual(prevalue, effectValue, effectCompare);
                    break;
                default:            // default: if it's none of the ones above
                    Debug.Log($"Number condition comparer does not support {preCompare} comparison");
                    break;
            }

            return success;
        }

        // we could run everything through the float (passing int in the function above), but integer comparisons are more efficient, that's why we create a different method to treat them
        // integer are a lower bit size, and so process a bit faster. And when we use a planner with a lot of AI, testing different conditions at once, optimisation is a essential when possible


        public static bool CompareNumberCondition(int prevalue, G_StateComparison preCompare, int effectValue, G_StateComparison effectCompare)     // that's an integer overload of CompareNumberCondition (previously declared)
        {
            bool success = false;

            switch (preCompare)
            {
                case G_StateComparison.equal:
                    success = TestEqual(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.greater:
                    success = TestGreater(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.greaterOrEqual:
                    success = TestGreaterOrEqual(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.lesser:
                    success = TestLesser(prevalue, effectValue, effectCompare);
                    break;
                case G_StateComparison.lesserOrEqual:
                    success = TestLesserOrEqual(prevalue, effectValue, effectCompare);
                    break;
                default:            // default: if it's none of the ones above
                    Debug.Log($"Number condition comparer does not support {preCompare} comparison");
                    break;
            }

            return success;
        }
        #endregion


        #region Test Equal
        static bool TestEqual(float prevalue, float effectValue, G_StateComparison effectCompare)
        {
            return effectCompare ==  G_StateComparison.equal && prevalue == effectValue;    // return true if effectCompare is the equal comparison, and prevalue = effectValue,
                                                                                            // return false otherwise
        }

        // Same thing here, we need a integer overload:

        static bool TestEqual(int prevalue, int effectValue, G_StateComparison effectCompare)
        {
            return effectCompare == G_StateComparison.equal && prevalue == effectValue;    // return true if effectCompare is the equal comparison, and prevalue = effectValue,
                                                                                           // return false otherwise
        }
        #endregion

        #region Test Greater
        static bool TestGreater(float prevalue, float effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue > prevalue;
                    break;
                case G_StateComparison.greater:
                    success = effectValue >=  prevalue;
                    break;
            case G_StateComparison.greaterOrEqual:
                    success = effectValue > prevalue;
                    break;
            }
            return success;
        }

        // Same thing here, we need a integer overload:

        static bool TestGreater(int prevalue, int effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue > prevalue;
                    break;
                case G_StateComparison.greater:
                    success = effectValue >= prevalue;
                    break;
                case G_StateComparison.greaterOrEqual:
                    success = effectValue > prevalue;
                    break;
            }
            return success;
        }
        #endregion

#region Test Lesser
        static bool TestLesser(float prevalue, float effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue < prevalue;
                    break;
                case G_StateComparison.lesser:
                    success = effectValue <= prevalue;
                    break;
                case G_StateComparison.lesserOrEqual:
                    success = effectValue < prevalue;
                    break;
            }
            return success;
        }

        // Same thing here, we need a integer overload:

        static bool TestLesser(int prevalue, int effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue < prevalue;
                    break;
                case G_StateComparison.lesser:
                    success = effectValue <= prevalue;
                    break;
                case G_StateComparison.lesserOrEqual:
                    success = effectValue < prevalue;
                    break;
            }
            return success;
        }
        #endregion


        #region Greater or Equal

        static bool TestGreaterOrEqual(float prevalue, float effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue >= prevalue;
                    break;
                case G_StateComparison.greater:
                    success = effectValue >= prevalue;
                    break;
                case G_StateComparison.greaterOrEqual:
                    success = effectValue >= prevalue;
                    break;
            }
            return success;
        }

        // Same thing here, we need a integer overload:

        static bool TestGreaterOrEqual(int prevalue, int effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue >= prevalue;
                    break;
                case G_StateComparison.greater:
                    success = effectValue >= prevalue - 1;  // -1 because of integer
                    break;
                case G_StateComparison.greaterOrEqual:
                    success = effectValue >= prevalue;
                    break;
            }
            return success;
        }
        #endregion


        #region Lesser or Equal
        static bool TestLesserOrEqual(float prevalue, float effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue <= prevalue;
                    break;
                case G_StateComparison.lesser:
                    success = effectValue <= prevalue;
                    break;
                case G_StateComparison.lesserOrEqual:
                    success = effectValue <= prevalue;
                    break;
            }
            return success;
        }

        // Same thing here, we need a integer overload:

        static bool TestLesserOrEqual(int prevalue, int effectValue, G_StateComparison effectCompare)
        {
            bool success = false;

            switch (effectCompare)
            {
                case G_StateComparison.equal:
                    success = effectValue <= prevalue;
                    break;
                case G_StateComparison.lesser:
                    success = effectValue <= prevalue +1;   // +1 because of integer
                    break;
                case G_StateComparison.lesserOrEqual:
                    success = effectValue <= prevalue;
                    break;
            }
            return success;
        }
        #endregion
    }
}
