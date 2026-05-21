using UnityEngine;
using GOAP;
using System.Collections.Generic;

namespace GOAP
{
    public static class G_Planner       // static as we'll have only one instance of this class / object
    {
        public static bool GeneratePlan(G_Goal goal, G_WorldState worldState, out List<G_Action> plan)
        {
            bool success = false;
            plan = new List<G_Action>();

            //initialise the node pool
            List<G_Node> nodePool = new List<G_Node>();

            //create a node for the goal
            G_Node rootnode = new G_Node(worldState.actionPool, goal.goalEffects, worldState);
            
            //add node to node pool
            nodePool.Add(rootnode);

            G_Node currentNode = null;

            //while plan not found
            while (true)
            {
                //  get cheapest node
                currentNode = nodePool[0];

                //  process node
                currentNode.ProcessNode();

                //  if node is successful
                if (currentNode.NodeState == G_NodeState.success)
                {
                    success = true;

                    // Return plan
                    plan = currentNode.ReturnPlan();

                    if (plan == null)
                    {
                        success = false;
                    }
                    // Break from loop
                    break;
                }
                //  else if plan failed
                else if (currentNode.NodeState == G_NodeState.failed)
                {
                    // Return empty plan
                    success = false;
                    
                    // Break from loop
                    break;
                }
                //   else
                else if (currentNode.NodeState == G_NodeState.closed)
                {
                    // Generate child nodes
                    nodePool.AddRange(currentNode.GenerateChildNodes());

                    // Sort the node loop

                    // Continue loop
                }
            }

            return success;
        }
    }

}