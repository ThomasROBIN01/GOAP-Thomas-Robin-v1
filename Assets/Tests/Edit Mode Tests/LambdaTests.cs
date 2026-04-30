using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LambdaTests
{
    // A Test behaves as an ordinary method
    [TestCase (true, "Jeremy", "Jeremy")]
    [TestCase(true, "Nicolas", "Nicolas")]
    [TestCase(true, "Thomas", "Thomas")]
    [TestCase(true, "Steve", null)]

    [TestCase(false, "Jeremy", "Jeremy")]
    [TestCase(false, "Nicolas", "Nicolas")]
    [TestCase(false, "Thomas", "Thomas")]
    [TestCase(false, "Steve", null)]
    public void ListFind(bool useFunctionVersion, string searchName, string expectedResult)
    {
        List<string> names = new List<string>();

        names.Add("Nicolas");
        names.Add("Jeremy");
        names.Add("Thomas");

        string foundMe = useFunctionVersion ? FindNameInList(names, searchName) : names.Find((name) => name == searchName);

        // string foundMe = useFunctionVersion ? FindNameInList(names, searchName);      // or we could that function as well

        Assert.AreEqual(expectedResult, foundMe);
    }

    string FindNameInList (List<string> list, string searchName)        // this is moslty if we need to reuse the function easily
    {
        return list.Find((name) => name == searchName);
    }
}
