using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class G_WorldStateBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";
        public List<G_State> states = new List<G_State>();
        public List<G_Action> actions = new List<G_Action>();
        public List<G_Goal> goals = new List<G_Goal>();


        // Constructor
        public G_WorldStateBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region With Functions
        // with functions
        public G_WorldStateBuilder WithState(G_State state)
        {
            if (states == null)
            {
                states = new List<G_State>(); 
            }

            states.Add(state);
            return this;
        }

        public G_WorldStateBuilder WithAction(G_Action action)
        {
            if (actions == null)
            {
                actions = new List<G_Action>();
            }

            actions.Add(action);
            return this;
        }

        public G_WorldStateBuilder WithGoal (G_Goal goal)
        {
            if (goals == null)
            {
                goals = new List<G_Goal>();
            }

            goals.Add(goal);
            return this;
        }

        #endregion

        #region ObjectCreation

        /// <summary>
        /// replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_WorldState Build()         
        {
            G_WorldState worldState = ScriptableObject.CreateInstance<G_WorldState>();
            worldState.Construct(states, actions, goals);
            return worldState;
        }

        public static implicit operator G_WorldState(G_WorldStateBuilder builder)        // This tells the compiler: “If someone tries to use a BuilderTemplate where an "object" type is expected… automatically call Build().”
                                                                                         // This triggers only when the compiler needs an "object" type.
        {
            return builder.Build();
        }

        #endregion

    }
}
