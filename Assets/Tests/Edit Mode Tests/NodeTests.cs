using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class NodeTests
{
    [TestCase(true, 0, 1, 1, TestName = "Goal Node")]
    [TestCase(false, 10, 3, 3, TestName = "Normal Node")]
    public void Constructor(bool testGoalNode, int hCost, int unmetCount, int preconCount)
    {
        GatherWoodTestData testData = new GatherWoodTestData();

        G_Node goalNode = new G_Node(testData.npc_world_state.actionPool, testData.gather_wood.goalEffects, testData.npc_world_state);

        G_Node normalNode = new G_Node(goalNode, testData.deliver_wood, goalNode.HCost, testData.npc_world_state.actionPool, goalNode.preconditions, testData.npc_world_state);

        G_Node testNode = testGoalNode ? goalNode : normalNode;     // if testNode = testGoalNode, then test Node = goalNode; if not testNode = normalNode

        if(testGoalNode)
        {
            testNode = goalNode;
        }
        else
        {
            testNode = normalNode;
        }

        Assert.NotNull(testNode);
        Assert.AreEqual(G_NodeState.open, testNode.NodeState);
        Assert.AreEqual(testGoalNode, testNode.ParentNode == null);
        Assert.AreEqual(testGoalNode, testNode.NodeAction == null);
        Assert.AreEqual(hCost, testNode.HCost);
        Assert.AreEqual(unmetCount, testNode.UnmetPreconditions);
        Assert.AreEqual(preconCount, testNode.preconditions.Count);
        Assert.NotNull(testNode.preconditions);
        Assert.AreEqual(testGoalNode, testNode.IsGoalNode);
    }

    [TestCase(1, TestName = "0 preconditions met by worldState")]
    [TestCase(2, TestName = "Some preconditions met by worldState")]
    [TestCase(3, TestName = "All preconditions met by worldState")]
    public void ProcessPreconditions(int preconsMet)
    {
        GatherWoodTestData testData = new GatherWoodTestData();

        G_Node goalNode = new G_Node(testData.npc_world_state.actionPool, testData.gather_wood.goalEffects, testData.npc_world_state);

        goalNode.preconditions[0].Meet();       // forcing it to be met to simulate correct planning

        G_Node normalNode = new G_Node(goalNode, 
                                       testData.deliver_wood, 
                                       goalNode.HCost, 
                                       testData.npc_world_state.actionPool, 
                                       goalNode.preconditions, 
                                       testData.npc_world_state,
                                       false);      // false here is to avoid running the if(processUnmetPreconditions) in the G_Node Constructor

        if (preconsMet >= 2)
        {
            testData.npc_inventory_component.AddToInventory(new ItemStack(testData.chopped_wood, 10));  // we meet one of the preconditions by changing the world state:
                                                                                                        // in this case we add to the inventory 10 chopped woods
        }
        if (preconsMet == 3)    // if = 3, we also go into the previous if >=2, so run both
        {
            // As all preconditions are met, we also change the location state:
            G_AtLocation locationState = testData.npc_world_state.states.Find((state) => state.name == testData.at_location.name) as G_AtLocation;

            locationState.SetValue(testData.woodstock);
        }

        int unmetPreconCount = normalNode.ProcessPreconditions(normalNode.preconditions, normalNode.WorldStateRef);
        int assertedRemainingPrecons = 3 -preconsMet;

        Assert.AreEqual(assertedRemainingPrecons, unmetPreconCount);   
    }

    [TestCase(G_NodeState.closed, TestName = "Closed")]
    [TestCase(G_NodeState.success, TestName = "Success")]
    [TestCase(G_NodeState.failed, TestName = "Failure")]
    public void ProcessNode(G_NodeState targetState)
    {
        GatherWoodTestData testData = new GatherWoodTestData();

        G_Node goalNode = new G_Node(testData.npc_world_state.actionPool, testData.gather_wood.goalEffects, testData.npc_world_state);

        G_Node normalNode = null;

        G_AtLocation locationState = null;

        switch (targetState)
        {
            case G_NodeState.success:

                goalNode.preconditions[0].Meet();

                // Success state: we change the world state to have the precondictions meet:
                testData.npc_inventory_component.AddToInventory(new ItemStack(testData.chopped_wood, 10));  // we meet one of the preconditions by changing the world state:
                                                                                                            // in this case we add to the inventory 10 chopped woods

                // As all preconditions are met, we also change the location state:
                locationState = testData.npc_world_state.states.Find((state) => state.name == testData.at_location.name) as G_AtLocation;

                locationState.SetValue(testData.woodstock);

                normalNode = new G_Node(goalNode,
                               testData.deliver_wood,
                               goalNode.HCost,
                               testData.npc_world_state.actionPool,
                               goalNode.preconditions,
                               testData.npc_world_state);
                break;

            case G_NodeState.closed:

                // Closed is the same as Success but run without unmet preconditions:
                testData.npc_inventory_component.AddToInventory(new ItemStack(testData.chopped_wood, 10));  // we meet one of the preconditions by changing the world state:
                                                                                                            // in this case we add to the inventory 10 chopped woods

                // As all preconditions are met, we also change the location state:
                locationState = testData.npc_world_state.states.Find((state) => state.name == testData.at_location.name) as G_AtLocation;

                locationState.SetValue(testData.woodstock);

                normalNode = new G_Node(goalNode,
                               testData.deliver_wood,
                               goalNode.HCost,
                               testData.npc_world_state.actionPool,
                               goalNode.preconditions,
                               testData.npc_world_state);
                break;

            case G_NodeState.failed:

                normalNode = new G_Node(goalNode,
                   testData.deliver_wood,
                   goalNode.HCost,
                   new List<G_Action>(),        // we basically rebuild the normal node to give it empty actions: 0 actions available
                   goalNode.preconditions,      // it should still have unmet preconditions
                   testData.npc_world_state);
                break;
        }

        normalNode.ProcessNode();

        Assert.AreEqual(targetState, normalNode.NodeState);

    }


    [TestCase(2, TestName = "Generates a node")]
    [TestCase(5, TestName = "Generates multiple nodes")]
    [TestCase(1, TestName = "Fails to generate any node")]
    public void GenerateChildNodes(int endNodeCount)
    {
        GatherWoodTestData testData = new GatherWoodTestData();

        List<G_Action> actionPool = testData.npc_world_state.actionPool;

        if(endNodeCount == 1)
        {
            actionPool.Clear();
        }

        G_Node goalNode = new G_Node(testData.npc_world_state.actionPool, testData.gather_wood.goalEffects, testData.npc_world_state);

        List<G_Node> nodePool = new List<G_Node>();
        nodePool.Add(goalNode);

        List<G_Node> tempNodes = goalNode.GenerateChildNodes();

        nodePool.AddRange(tempNodes);

        if (endNodeCount == 5)
        {
            tempNodes = nodePool[1].GenerateChildNodes();
            nodePool.AddRange(tempNodes);

            for (int i = 0; i < nodePool.Count; i++)
            {
                if(nodePool[i].IsGoalNode)
                {
                    continue;
                }
                Debug.Log($"{nodePool[i].NodeAction.name}");
            }
        }

        Assert.AreEqual(endNodeCount, nodePool.Count);
    }


    [TestCase(false, TestName = "Standard Plan")]
    [TestCase(true, TestName = "Null action in middle")]
    public void ReturnPlan(bool hasNullAction)
    {
        GatherWoodTestData testData = new GatherWoodTestData();

        G_Node goalNode = new G_Node(testData.npc_world_state.actionPool, testData.gather_wood.goalEffects, testData.npc_world_state);

        G_Node deliver_wood_node = new G_Node(goalNode, testData.deliver_wood, goalNode.HCost, testData.npc_world_state.actionPool, goalNode.preconditions, testData.npc_world_state);

        G_Node go_to_woodstock_node = new G_Node(deliver_wood_node, 
                                                 testData.go_to_woodstock,
                                                 deliver_wood_node.HCost,
                                                 testData.npc_world_state.actionPool,
                                                 deliver_wood_node.preconditions,
                                                 testData.npc_world_state);

        G_Node chop_tree_node = new G_Node(go_to_woodstock_node,
                                           testData.chop_tree,
                                           go_to_woodstock_node.HCost,
                                           testData.npc_world_state.actionPool,
                                           go_to_woodstock_node.preconditions,
                                           testData.npc_world_state);

        G_Node go_to_tree_node = new G_Node(chop_tree_node,
                                            hasNullAction ? null : testData.go_to_tree,     // if hasNullAction is true, this parameter = null, if false, we'll use testData.go_to_tree
                                            chop_tree_node.HCost,
                                            testData.npc_world_state.actionPool,
                                            chop_tree_node.preconditions,
                                            testData.npc_world_state);

        G_Node take_axe_node = new G_Node(go_to_tree_node,
                                          testData.take_axe,
                                          go_to_tree_node.HCost,
                                          testData.npc_world_state.actionPool,
                                          go_to_tree_node.preconditions,
                                          testData.npc_world_state);

        G_Node go_to_workshop_node = new G_Node(take_axe_node,
                                                testData.go_to_workshop,
                                                take_axe_node.HCost,
                                                testData.npc_world_state.actionPool,
                                                take_axe_node.preconditions,
                                                testData.npc_world_state);


        List<G_Action> plan = go_to_workshop_node.ReturnPlan();

        /*
            public G_Action deliver_wood;
            public G_Action go_to_woodstock;
            public G_Action chop_tree;
            public G_Action go_to_tree;
            public G_Action take_axe;
            public G_Action go_to_workshop;
        */

        if (!hasNullAction)
        {
            Assert.AreEqual(true, plan != null);
            Assert.AreEqual(6, plan.Count);

            Assert.AreEqual("deliver_wood", plan[0].name);
            Assert.AreEqual("go_to_woodstock", plan[1].name);
            Assert.AreEqual("chop_tree", plan[2].name);
            Assert.AreEqual("go_to_tree", plan[3].name);
            Assert.AreEqual("take_axe", plan[4].name);
            Assert.AreEqual("go_to_workshop", plan[5].name);
        }
        else
        {
            Assert.AreEqual(true, plan == null);
        }
        

    }
}
