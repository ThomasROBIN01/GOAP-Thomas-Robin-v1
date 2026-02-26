using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;     // to use our own classes, like G_BoolState

public class BoolStateTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Construct()
    {
        // Use the Assert class to test conditions

        G_BoolState testState= ScriptableObject.CreateInstance<G_BoolState>();  // Everytime we are creating a new object, we create a new instance, and then construct it (below)

        testState.Construct("test", true);

        testState = A.BoolState().WithName("test").WithValue(true);     // Creation of a bool state builder using chaining (with the ".").
                                                                        // As testState is a BoolState type, this triggers the automatic Build from G_BoolStateBuilder, 
                                                                        // and we don't need to Build it here: we don't need the testState.Build() as below:
     // testState = A.BoolState().WithName("test").WithValue(true).Build();

        // if we didn't use the "return,this" in the withName and withValue in the G_BoolStateBuilder, but instead having them as void functions (returning nothing), we would have to do this:
        //G_BoolStateBuilder builder = A.BoolState();
        //builder.WithName("test");
        //builder.WithValue(true);
        //testState = builder.Build();
        // that's much more cumbersome

        Assert.AreEqual("test", testState.name);
        Assert.AreEqual(true, (bool)testState.GetValue());
    }

    [Test]
    public void Clone()
    {
        G_BoolState testState = ScriptableObject.CreateInstance<G_BoolState>();
        testState.Construct("test", true);

        G_State cloneState = testState.Clone();

        Assert.AreEqual(testState.name, cloneState.name);       // the name are the same
        Assert.AreEqual((bool)testState.GetValue(), (bool)cloneState.GetValue());       // the value are the same
        Assert.IsTrue(cloneState is G_BoolState);       // even though it's stored as a base class type, the clone function should generate a boolstate (the value should be from the inheritating class)
    }


    [TestCase(true, G_StateComparison.equal, true, true)]           // TestCase to enter an argument. TestCase allows us to run different test by changing the arguments we pass in it
    [TestCase(false, G_StateComparison.equal, true, false)]
    [TestCase(true, G_StateComparison.not_equal, false, true)]
    [TestCase(false, G_StateComparison.not_equal, false, false)]
    public void TestState(bool actualValue, G_StateComparison comparison, bool expectedValue, bool expectedResult)
    {
        G_BoolState testState = ScriptableObject.CreateInstance<G_BoolState>();
        testState.Construct("test", actualValue);

        bool result = testState.TestState(testState, comparison, expectedValue);

        Assert.AreEqual(expectedResult, result);


    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator BoolStateTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}

}
