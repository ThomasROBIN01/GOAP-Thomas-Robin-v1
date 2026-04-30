using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GOAP
{
    public class G_Node
    {
        #region Data

        G_NodeState nodeState = G_NodeState.open;
        public G_NodeState NodeState
        {
            get { return nodeState; } 
        }

        G_Node parentNode;
        public G_Node Parent
        {
            get { return parentNode; }
        }

        G_Action nodeAction;
        public G_Action NodeAction
        {
            get { return nodeAction; }
        }

        int hCost = 0;
        public int HCost
        { get { return hCost; } }

        int unmetPreconditions = 0;
        public int UnmetPreconditions
        { get { return unmetPreconditions; } }


        List<G_Action> nodeActionPool = new List<G_Action>();

        public List<G_Condition> preconditions = new List<G_Condition>();

        G_WorldState worldStateRef;

        bool isGoalNode = false;
        public bool IsGoalNode
        {
            get { return isGoalNode; } 
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor for Actions
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeAction"></param>
        /// <param name="hCost"></param>
        /// <param name="nodeActionPool"></param>
        /// <param name="preconditions"></param>
        /// <param name="worldStateRef"></param>
        public G_Node(G_Node parentNode, G_Action nodeAction, int hCost, List<G_Action> nodeActionPool, List<G_Condition> preconditions, G_WorldState worldStateRef)
        {
            // parent node
            this.parentNode = parentNode;

            // action
            this.nodeAction = nodeAction;

            // hCost
            this.hCost = hCost + nodeAction.GetCost();

            // action pool
            this.nodeActionPool = new List<G_Action>(nodeActionPool);       // it will create a clone of the list nodeActionPool, so like this, we don't modify it
            this.nodeActionPool.Remove(this.nodeAction);                    // make sure we remove the action, so we don't reuse and replan it

            // preconditions
            this.preconditions = new List<G_Condition>(preconditions);      // same thing here: creatign a clone that can be modified, without modifying the orginal preconditionc

            // world state reference
            this.worldStateRef = worldStateRef;

            nodeState = G_NodeState.open;               // initialising to make sure the nodeState is opened

            // determine unmet preconditions
            this.unmetPreconditions = ProcessPreconditions(this.preconditions, this.worldStateRef);
        }

        /// <summary>
        /// Use this overload for creating the Goal Node
        /// </summary>
        /// <param name="nodeActionPool"></param>
        /// <param name="preconditions"></param>
        /// <param name="worldStateRef"></param>
        public G_Node(List<G_Action> nodeActionPool, List<G_Condition> preconditions, G_WorldState worldStateRef)
        {
            // action pool
            this.nodeActionPool = new List<G_Action>(nodeActionPool);       // it will create a clone of the list nodeActionPool, so like this, we don't modify it

            // preconditions
            this.preconditions = new List<G_Condition>(preconditions);      // same thing here: creatign a clone that can be modified, without modifying the orginal preconditionc

            // world state reference
            this.worldStateRef = worldStateRef;

            nodeState = G_NodeState.open;               // initialising to make sure the nodeState is opened

            // determine unmet preconditions
            this.unmetPreconditions = ProcessPreconditions(this.preconditions, this.worldStateRef);
            isGoalNode = true;
        }

        #endregion

        #region Functions
        /// <summary>
        /// Test the list of preconditions and test them against the current world state to see if they are met or not
        /// If a precondtion is met by the world state, we will set it to Met and add 1 to the unmetCount
        /// </summary>
        /// <param name="preConditions"></param>
        /// <param name="worldStateRef"></param>
        /// <returns></returns>
        public int ProcessPreconditions(List<G_Condition> preconditions, G_WorldState worldStateRef)
        {
            int unmetCount = 0;

            for (int i = 0; i<preconditions.Count; i++)
            {
               if (!preconditions[i].Met)
                {
                    ProcessPreconditions(preconditions[i], ref unmetCount);
                }
            }

            return unmetCount;
        }

        void ProcessPreconditions(G_Condition precondition, ref int unmetCount)
        {

            G_State stateRef = worldStateRef.states.Find((state) => precondition.IsStateTheConditionState(state));  // we compare the current precondition with all of the states in the world stateto see the state matches,
                                                                                                                    // and if it does, it will return it and store it in stateRef

            if (stateRef != null && precondition.DoesStateMeetCondition(stateRef))      // is this condition true within this world state
            {
                precondition.Meet();
            }
            if (!precondition.Met)      // if it hasn't been met
            {
                unmetCount += 1;
            }

        }

        #endregion
    }
}