using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class G_GoalBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";
        int priority = 0;
        List<G_Condition> triggers = new List<G_Condition>();
        List<G_Condition> effects = new List<G_Condition>();

        // Constructor
        public G_GoalBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region With Functions

        public G_GoalBuilder WithPriority(int priority)
        {
            this.priority = priority;
            return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                // Instead, using return.this allows to chain on to the end of it. 
        }

        public G_GoalBuilder WithTrigger(G_Condition trigger)
        {
            if (triggers == null)
            {
                triggers = new List<G_Condition>();
            }

            triggers.Add(trigger);
            return this;
        }

        public G_GoalBuilder WithEffect(G_Condition effect)
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
        public G_Goal Build()         
        {
            G_Goal goal = ScriptableObject.CreateInstance<G_Goal>();
            goal.Construct(name, triggers, effects, priority);
            return goal;
        }

        public static implicit operator G_Goal(G_GoalBuilder builder)
        {
            return builder.Build();
        }
        #endregion

    }
}
