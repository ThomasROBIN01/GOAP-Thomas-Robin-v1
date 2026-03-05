using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class IntStateTests
{
    [Test]
    public void Clone()
    {
        //G_IntState testState = ScriptableObject.CreateInstance<G_BoolState>();
        //testState.Construct("test", true);

        // We don't need the 2 previous lines anymore as the one below is doing this in a more readable manner:

        G_IntState testState = An.IntState().WithName("test").WithValue(5);     // Creation of a int state builder using chaining (with the ".").
                                                                                // As testState is a IntState type, this triggers the automatic Build from G_IntStateBuilder, 
                                                                                // and we don't need to Build it here: we don't need the testState.Build() as below:
                                                                                // testState = An.IntState().WithName("test").WithValue(true).Build();

        G_State cloneState = testState.Clone();

        Assert.AreEqual(testState.name, cloneState.name);       // the name are the same
        Assert.AreEqual((int)testState.GetValue(), (int)cloneState.GetValue());       // the value are the same
        Assert.IsTrue(cloneState is G_IntState);       // even though it's stored as a base class type, the clone function should generate a floatstate (the value should be from the inheritating class)
    }


    // A Test behaves as an ordinary method
    [TestCase(5, G_StateComparison.equal, 5, true, TestName = "Equals - True")]
    [TestCase(5, G_StateComparison.equal, 0, false, TestName = "Equals - False")]

    [TestCase(5, G_StateComparison.greater, 4, true, TestName = "Greater - Lesser Test Value")]
    [TestCase(5, G_StateComparison.greater, 5, false, TestName = "Greater - Equal Test Value")]
    [TestCase(5, G_StateComparison.greater, 6, false, TestName = "Greater - Greater Test Value")]

    [TestCase(5, G_StateComparison.greaterOrEqual, 4, true, TestName = "Greater or Equal - Lesser Test Value")]
    [TestCase(5, G_StateComparison.greaterOrEqual, 5, true, TestName = "Greater or Equal - Equal Test Value")]
    [TestCase(5, G_StateComparison.greaterOrEqual, 6, false, TestName = "Greater or Equal - Greater Test Value")]

    [TestCase(5, G_StateComparison.lesser, 4, false, TestName = "Lesser - Lesser Test Value")]
    [TestCase(5, G_StateComparison.lesser, 5, false, TestName = "Lesser - Equal Test Value")]
    [TestCase(5, G_StateComparison.lesser, 6, true, TestName = "Lesser - Greater Test Value")]

    [TestCase(5, G_StateComparison.lesserOrEqual, 4, false, TestName = "Lesser or Equal - Lesser Test Value")]
    [TestCase(5, G_StateComparison.lesserOrEqual, 5, true, TestName = "Lesser or Equal - Equal Test Value")]
    [TestCase(5, G_StateComparison.lesserOrEqual, 6, true, TestName = "Lesser or Equal - Greater Test Value")]


    public void TestState(int stateValue, G_StateComparison comparison, int testValue, bool expectedResult)
    {
        G_IntState testState = An.IntState().WithName("test").WithValue(stateValue);

        bool result = testState.TestState(testState, comparison, testValue);
        Assert.AreEqual(expectedResult, result);
    }
}
