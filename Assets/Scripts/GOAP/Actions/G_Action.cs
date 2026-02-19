using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [CreateAssetMenu(fileName = "G_Action", menuName = "Scriptable Objects/G_Action")]
    public class G_Action : ScriptableObject
    {
        #region Values
        internal int cost = 10;    // internal means it's accessible internally within this class and its driving classes, because we will make child classes of G_Action

        public List<G_Condition> preconditions = new List<G_Condition>();

        internal List<G_Condition> effects = new List<G_Condition>();

        public void Construct (string name,
            List<G_Condition> preconditions,
            List<G_Condition> effects,
            int cost = 10)
        {
            this.name = name;
            this.preconditions = preconditions;
            this.effects = effects;
            this.cost = cost;
        }
        #endregion

        #region Planning Functions

        /// <summary>
        /// Receives a bunch of unmet preconditions and returns a list of any of them that are met by this Action's effect
        /// </summary>
        /// <param name="unmetPreconditions"></param>
        /// <returns></returns>
        public List<G_Condition> TestEffectsAgainstPreconditions (List<G_Condition> unmetPreconditions)
        {
            List<G_Condition> metConditions = new List<G_Condition>();

            for (int i = 0; i < unmetPreconditions.Count; i++)
            {
                if (DoesEffectMatch(unmetPreconditions[i]))
                {
                    MeetCondition(unmetPreconditions[i], metConditions);
                }
            }


            return metConditions;
        }

        /// <summary>
        /// Clone the given condition, sets it as met, and then adds it to the metCondition list
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="metConditions"></param>
        void MeetCondition (G_Condition condition, List<G_Condition> metConditions)
        {
            G_Condition clone = G_Condition.Clone(condition);    // we clone it, like this we don't modify the one in the planner
            clone.Meet();
            metConditions.Add(clone);
        }

        /// <summary>
        /// Returns the first (if any) effect that has the same state as the unmetPrecondition and which succeeds at a Condition Comparison test
        /// </summary>
        /// <param name="unmetPrecondition"></param>
        /// <returns></returns>
        public bool DoesEffectMatch (G_Condition unmetPrecondition)
        {
            G_Condition relevantEffect = effects.Find(      // "Find" iterates through every item in the list. it requires a function argument that takes a parameter of 1 of the item in the list (G_Condition for example) and then compare and return true if the conditions are met.
                (effect) => effect.IsStateTheConditionState(unmetPrecondition.State)        // we check if the name match // anonymous method created within a line thanks to the "=>". On the left of it are the parameters required for the anonymous method (in this case the parameter name (effect)), and on the right side of the "=>" is the method body
                && unmetPrecondition.CompareConditionToEffect(effect)                       // check if the CompareConditionToEffect is true
                );                                                                          // if both parameters are true, what's inside the Find function will return true, and the Find function will be successful and will return the first item in the list that meet the conditions
            
            return relevantEffect != null;
        }

        /// <summary>
        /// Creates a duplicate of this action instance
        /// </summary>
        /// <returns></returns>
        public virtual G_Action Clone ()
        {
            G_Action clonedAction = ScriptableObject.CreateInstance<G_Action>();
            List<G_Condition> clonedPreconditions = new List<G_Condition>();
            List<G_Condition> clonedEffects = new List<G_Condition>();

            for (int i=0; i < this.preconditions.Count; i++)
            {
                clonedPreconditions.Add(G_Condition.Clone(preconditions[i]));
            }

            for (int i = 0; i < this.effects.Count; i++)
            {
                clonedEffects.Add(G_Condition.Clone(effects[i]));
            }

            clonedAction.Construct(this.name,
                clonedPreconditions,
                clonedEffects,
                this.cost);

            return clonedAction;
        }

        // STOPPED AT 4b. Action Planning Functions

        #endregion

        #region Behaviour

        /// <summary>
        /// Called when the action is first run by the NPC with the NPC's gameObject passed in as parameter
        /// </summary>
        /// <param name="npcObject"></param>
        public virtual void StartAction (GameObject npcObject)
        {

        }

        /// <summary>
        /// Called during Update every frame by the NPC object with GameObject of the NPC passed in as a parameter
        /// </summary>
        /// <param name="npcObject"></param>
        public virtual void UpdateAction (GameObject npcObject)
        {

        }

        /// <summary>
        /// Called internally in the behaviour when the action is finished and will send a callback to the NPC
        /// </summary>
        /// <param name="success"></param>
        internal virtual void EndAction (bool success)
        {

        }


        #endregion
    }
}
