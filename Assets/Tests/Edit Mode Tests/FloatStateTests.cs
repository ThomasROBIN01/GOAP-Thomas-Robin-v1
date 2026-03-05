using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class FloatStateTests
{
    [Test]
    public void Clone()
    {
        //G_FloatState testState = ScriptableObject.CreateInstance<G_BoolState>();
        //testState.Construct("test", true);

        // We don't need the 2 previous lines anymore as the one below is doing this in a more readable manner:

        G_FloatState testState = A.FloatState().WithName("test").WithValue(5);     // Creation of a float state builder using chaining (with the ".").
                                                                                    // As testState is a FloatState type, this triggers the automatic Build from G_FloatStateBuilder, 
                                                                                    // and we don't need to Build it here: we don't need the testState.Build() as below:
                                                                                    // testState = A.FloatState().WithName("test").WithValue(true).Build();

        G_State cloneState = testState.Clone();

        Assert.AreEqual(testState.name, cloneState.name);       // the name are the same
        Assert.AreEqual((float)testState.GetValue(), (float)cloneState.GetValue());       // the value are the same
        Assert.IsTrue(cloneState is G_FloatState);       // even though it's stored as a base class type, the clone function should generate a floatstate (the value should be from the inheritating class)
    }


    // A Test behaves as an ordinary method
    [TestCase(5f, G_StateComparison.equal, 5, true, TestName = "Equals - True")]
    [TestCase(5f, G_StateComparison.equal, 0, false, TestName = "Equals - False")]

    [TestCase(5f, G_StateComparison.greater, 4, true, TestName = "Greater - Lesser Test Value")]
    [TestCase(5f, G_StateComparison.greater, 5, false, TestName = "Greater - Equal Test Value")]
    [TestCase(5f, G_StateComparison.greater, 6, false, TestName = "Greater - Greater Test Value")]

    [TestCase(5f, G_StateComparison.greaterOrEqual, 4, true, TestName = "Greater or Equal - Lesser Test Value")]
    [TestCase(5f, G_StateComparison.greaterOrEqual, 5, true, TestName = "Greater or Equal - Equal Test Value")]
    [TestCase(5f, G_StateComparison.greaterOrEqual, 6, false, TestName = "Greater or Equal - Greater Test Value")]

    [TestCase(5f, G_StateComparison.lesser, 4, false, TestName = "Lesser - Lesser Test Value")]
    [TestCase(5f, G_StateComparison.lesser, 5, false, TestName = "Lesser - Equal Test Value")]
    [TestCase(5f, G_StateComparison.lesser, 6, true, TestName = "Lesser - Greater Test Value")]

    [TestCase(5f, G_StateComparison.lesserOrEqual, 4, false, TestName = "Lesser or Equal - Lesser Test Value")]
    [TestCase(5f, G_StateComparison.lesserOrEqual, 5, true, TestName = "Lesser or Equal - Equal Test Value")]
    [TestCase(5f, G_StateComparison.lesserOrEqual, 6, true, TestName = "Lesser or Equal - Greater Test Value")]


    public void TestState(float stateValue, G_StateComparison comparison, float testValue, bool expectedResult)
    {
        G_FloatState testState = A.FloatState().WithName("test").WithValue(stateValue);

        bool result = testState.TestState(testState, comparison, testValue);
        Assert.AreEqual(expectedResult, result);
    }


}
