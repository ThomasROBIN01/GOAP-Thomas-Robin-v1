using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class AtLocationTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void AtLocationClone()
    {
        // Use the Assert class to test conditions
    }


    [TestCase(true, TestName = "Tree vs Tree")]
    [TestCase(true, TestName = "Null vs Null")]
    [TestCase(true, TestName = "Tree vs Null")]
    [TestCase(true, TestName = "Null vs Tree")]
    public void AtLocationTestState(bool expectedResult)
    {
        // Use the Assert class to test conditions
    }


    [TestCase(true, TestName = "Equals tree vs Equals tree")]
    [TestCase(true, TestName = "Equals null vs Equals null")]
    [TestCase(false, TestName = "Equals null vs Equals tree")]
    [TestCase(false, TestName = "Equals tree vs Equals null")]

    [TestCase(false, TestName = "Greater tree vs Equals tree")]
    [TestCase(false, TestName = "Greater null vs Equals null")]
    public void AtLocationConditionCompare(bool expectedResult)
    {
        // Use the Assert class to test conditions
    }

}
