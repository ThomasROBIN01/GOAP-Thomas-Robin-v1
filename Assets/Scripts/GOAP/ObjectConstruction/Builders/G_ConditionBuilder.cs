using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class G_ConditionBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        G_State state;
        G_StateComparison comparison = G_StateComparison.equal;
        object expectedValue;
        bool met = false;

        // Constructor
        public G_ConditionBuilder()
        {

        }
        #endregion

        #region With Functions
        // with functions
        public G_ConditionBuilder WithState(G_State state)
        {
            this.state = state;
            return this;
        }

        public G_ConditionBuilder WithComparison(G_StateComparison comparison)
        {
            this.comparison = comparison;
            return this;
        }

        public G_ConditionBuilder WithExpectedValue(object expectedValue)
        {
            this.expectedValue = expectedValue;
            return this;
        }

        public G_ConditionBuilder WithMet(bool met)
        {
            this.met = met;
            return this;
        }

        #endregion

        #region ObjectCreation

        /// <summary>
        /// replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_Condition Build()         
        {
            G_Condition condition = new G_Condition(state, expectedValue, comparison, met);
            return condition;
        }

        public static implicit operator G_Condition (G_ConditionBuilder builder)  // This tells the compiler: “If someone tries to use a G_ConditionBuilder where an "G_Condition" type is expected… automatically call Build().”
                                                                                  // This triggers only when the compiler needs an "G_Condition" type.
        {
            return builder.Build();
        }
        #endregion

    }
}
