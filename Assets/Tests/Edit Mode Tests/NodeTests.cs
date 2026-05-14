using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class NodeTests
{
    // Process preconditions test - checking for fulfilled preconditions from the world state


    // Process Node - get the node's planning result



    // Generate child modes



    // Return plan - return the whole plan as a list


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

    [TestCase(TestName = "0 preconditions met by worldState")]
    [TestCase(TestName = "Some preconditions met by worldState")]
    public void ProcessPreconditions()
    {

    }

    [TestCase(TestName = "Closed")]
    [TestCase(TestName = "Success")]
    [TestCase(TestName = "Failure")]
    public void ProcessNode()
    {

    }


    [TestCase(TestName = "Generates several nodes")]
    [TestCase(TestName = "Fails to generate any node")]
    public void GenerateChildNodes()
    {

    }

    [TestCase(TestName = "Standard Plan")]
    [TestCase(TestName = "Null action in middle")]
    public void ReturnPlan()
    {

    }
}
