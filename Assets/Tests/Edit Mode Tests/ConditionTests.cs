using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class ConditionTests
{
    #region Condition Fuctions

    [TestCase(false, TestName = "Use Internal State")]
    [TestCase(true, TestName = "Use Parameter State")]
    public void DoesStateMeetCondition(bool useParameter)
    {
        G_BoolState boolState = A.BoolState().WithName("test").WithValue(true);

        G_Condition condition = A.Condition().WithState(boolState).WithComparison(G_StateComparison.equal).WithExpectedValue(true);

        bool result = false;

        if (useParameter)
        {
            result = condition.DoesStateMeetCondition(boolState);
        }
        else
        {
            result = condition.DoesStateMeetCondition();
        }

        Assert.IsTrue(result);
    }

    #endregion

    #region Bool Conditions

    // Equals
    [TestCase(G_StateComparison.equal, true, G_StateComparison.equal, true, true, TestName = "Equal True vs Equal True")]
    [TestCase(G_StateComparison.equal, true, G_StateComparison.equal, false, false, TestName = "Equal True vs Equal False")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.equal, true, false, TestName = "Equal False vs Equal True")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.equal, false, true, TestName = "Equal False vs Equal False")]

    // Not Equals
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.not_equal, true, true, TestName = "Not Equal True vs Not Equal True")]
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.not_equal, false, false, TestName = "Not Equal True vs Not Equal False")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.not_equal, true, false, TestName = "Not Equal False vs Not Equal True")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.not_equal, false, true, TestName = "Not Equal False vs Not Equal False")]

    // Equal vs Not Equal
    [TestCase(G_StateComparison.equal, true, G_StateComparison.not_equal, true, false, TestName = "Equal True vs Not Equal True")]
    [TestCase(G_StateComparison.equal, true, G_StateComparison.not_equal, false, true, TestName = "Equal True vs Not Equal False")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.not_equal, true, true, TestName = "Equal False vs Not Equal True")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.not_equal, false, false, TestName = "Equal False vs Not Equal False")]

    // Not Equals vs Equal
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.equal, true, false, TestName = "Not Equal True vs Equal True")]
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.equal, false, true, TestName = "Not Equal True vs Equal False")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.equal, true, true, TestName = "Not Equal False vs Equal True")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.equal, false, false, TestName = "Not Equal False vs Equal False")]


    public void CompareConditionToEffect_Bool(G_StateComparison preComparison,
        bool preExpectedValue,
        G_StateComparison effectComparison,         
        bool effectExpectedValue,
        bool expectedResult)
    {
        // Arrange
        G_BoolState boolState = A.BoolState().WithName("test").WithValue(true);

        G_Condition preCondition = A.Condition().WithState(boolState).WithComparison(preComparison).WithExpectedValue(preExpectedValue);

        G_Condition effect = A.Condition().WithState(boolState).WithComparison(effectComparison).WithExpectedValue(effectExpectedValue);


        // Act
        bool result = preCondition.CompareConditionToEffect(effect);


        // Assert
        Assert.AreEqual(expectedResult, result);
    }
    #endregion

    #region Float Condition

        #region Equals

    [TestCase(G_StateComparison.equal, 5, G_StateComparison.equal, 5, true, TestName = "Equal 5 vs Equal 5")]

    [TestCase(G_StateComparison.equal, 5, G_StateComparison.equal, 4, false, TestName = "Equal 5 vs Equal 4")]

        #endregion

        #region Greater

            #region Vs Equal
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 4, false, TestName = "Precondition > 5 vs Effect = 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 5, false, TestName = "Precondition > 5 vs Effect = 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 6, true, TestName = "Precondition > 5 vs Effect = 6")]
            #endregion

            #region Vs Greater
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 4, false, TestName = "Precondition > 5 vs Effect > 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 5, true, TestName = "Precondition > 5 vs Effect > 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 6, true, TestName = "Precondition > 5 vs Effect > 6")]
            #endregion

            #region Vs Greater or Equal
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greaterOrEqual, 4, false, TestName = "Precondition > 5 vs Effect >= 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greaterOrEqual, 5, false, TestName = "Precondition > 5 vs Effect >= 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greaterOrEqual, 6, true, TestName = "Precondition > 5 vs Effect >= 6")]
    #endregion

    #endregion


    #region Lesser

    #region Vs Equal
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 4, true, TestName = "Precondition < 5 vs Effect = 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 5, false, TestName = "Precondition < 5 vs Effect = 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 6, false, TestName = "Precondition < 5 vs Effect = 6")]
    #endregion

    #region Vs Lesser
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 4, true, TestName = "Precondition < 5 vs Effect < 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 5, true, TestName = "Precondition < 5 vs Effect < 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 6, false, TestName = "Precondition < 5 vs Effect < 6")]
    #endregion

    #region Vs Lesser or Equal
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesserOrEqual, 4, true, TestName = "Precondition > 5 vs Effect <= 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesserOrEqual, 5, false, TestName = "Precondition > 5 vs Effect <= 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesserOrEqual, 6, false, TestName = "Precondition > 5 vs Effect <= 6")]
    #endregion

    #endregion

    public void CompareConditionToEffect_Float(G_StateComparison preComparison,
        float preExpectedValue,
        G_StateComparison effectComparison,
        float effectExpectedValue,
        bool expectedResult)
    {
        // Arrange
        G_FloatState floatState = A.FloatState().WithName("test").WithValue(5);

        G_Condition preCondition = A.Condition().WithState(floatState).WithComparison(preComparison).WithExpectedValue(preExpectedValue);

        G_Condition effect = A.Condition().WithState(floatState).WithComparison(effectComparison).WithExpectedValue(effectExpectedValue);


        // Act
        bool result = preCondition.CompareConditionToEffect(effect);


        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    #endregion



    #region Int Condition

    #endregion

}
