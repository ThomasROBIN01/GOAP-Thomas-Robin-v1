using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class NodeTests
{
    // constructor for normal nodes and for goal nodes


    // Process preconditions test - checking for fulfilled preconditions from the world state


    // Process Node - get the node's planning result



    // Generate child modes



    // Return plan - return the whole plan as a list


    [TestCase(TestName = "Goal Node")]
    [TestCase(TestName = "Normal Node")]
    public void Constructor()
    {
        GatherWoodTestData testData = new GatherWoodTestData();

        G_Node goalNode = new G_Node(testData.npc_world_state.actionPool, testData.gather_wood.goalEffects, testData.npc_world_state);

        Assert.NotNull(goalNode);
        Assert.AreEqual(G_NodeState.open, goalNode.NodeState);
        Assert.AreEqual(null, goalNode.ParentNode);
        Assert.AreEqual(null, goalNode.NodeAction);
        Assert.AreEqual(0, goalNode.HCost);
        Assert.AreEqual(1, goalNode.UnmetPreconditions);
        Assert.NotNull(goalNode.preconditions);
        Assert.AreEqual(goalNode.preconditions.Count, 1);
        Assert.AreEqual(true, goalNode.IsGoalNode);
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
