using UnityEngine;
using GOAP;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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

            int poolCounter = 0;

            //while plan not found
            while (true)
            {
                poolCounter++;
                // Debug.Log($"Iteration: {poolCounter} =============================================");

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
                    nodePool = SortPool(nodePool);

                    //for (int i = 0; i < nodePool.Count; i++)
                    //{
                    //    if (nodePool[i].IsGoalNode)
                    //    {
                    //        Debug.Log($"Node: {i} goal node");
                    //    }
                    //    else
                    //    {
                    //        Debug.Log($"Node: {i} {nodePool[i].NodeAction.name} state {nodePool[i].NodeState} cost {nodePool[i].HCost}");
                    //    }
                    //}

                    if (nodePool[0].NodeState != G_NodeState.open)        // if after sorting the nodes, the first one is not opened, this means there is no opened node left, so it fails
                    {
                        success = false;
                        break;
                    }

                    // Continue loop
                }
            }
            return success;
        }


        public static List<G_Node> SortPool(List<G_Node> pool)
        {
            return pool.OrderBy((node) => node.NodeState)       // node is a parameter name representing each node in the pool                                                                
                                                                // OrderBy will use the number that have been attributed in the G_NodeState class:
                                                                //      open = 0, closed = 1, failed = 2, success = 3
                .ThenBy((node) => node.Priority)        // so we order these nodeS by state, then by priority,
                .ThenBy((node) => node.HCost)           // then by hCost
                                                        // the reason we order them by state first, is to avoid process any node that has already been processed

                .ToList();         // node is a G_Node, so we need to convert it to a List with ToList() to be able to use that OrderBy method
        }

    }

}