using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class G_ActionBuilder
    {
        #region Basic Values

        // any values to be transferred into the build G_Action
        string name = "";
        List<G_Condition> preconditions = new List<G_Condition>();
        List<G_Condition> effects = new List<G_Condition>();
        int cost = 10;

        // Constructor
        public G_ActionBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region With Functions

        public G_ActionBuilder WithCost(int cost)
        {
            this.cost = cost;
            return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                // Instead, using return.this allows to chain on to the end of it. 
        }

        public G_ActionBuilder WithPrecondition(G_Condition precondition)
        {
            if (preconditions == null)
            {
                preconditions = new List<G_Condition>();
            }

            preconditions.Add(precondition);
            return this;
        }

        public G_ActionBuilder WithEffect(G_Condition effect)
        {
            if (effects == null)
            {
                effects = new List<G_Condition>();
            }

            effects.Add(effect);
            return this;
        }

        #endregion

        #region ObjectCreation

        /// <summary>
        /// replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_Action Build()         
        {
            G_Action action = ScriptableObject.CreateInstance<G_Action>();
            action.Construct(name, preconditions, effects, cost);

            return action;
        }

        public static implicit operator G_Action(G_ActionBuilder builder)        // This tells the compiler: “If someone tries to use a G_ActionBuilder where an "G_Action" type is expected… automatically call Build().”
                                                                                 // This triggers only when the compiler needs an "G_Action" type.
        {
            return builder.Build();
        }
        // We got an error with this function because we can't convert an G_Action to a G_ActionBuilder, so we just comment this for now
        // and will uncomment and change this depending on the class builder

        #endregion

    }
}
