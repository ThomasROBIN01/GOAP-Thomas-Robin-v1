using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [CreateAssetMenu(fileName = "G_Action", menuName = "Scriptable Objects/G_Action")]
    public class G_Action : ScriptableObject
    {
        #region Values
        internal int cost = 10;    // internal means it's accessible internally within this class and its driving classes, because we will make child classes of G_Action

        public List<G_Condition> preconditions = new List<G_Condition>();

        internal List<G_Condition> effects = new List<G_Condition>();

        public void Construct (string name,
            List<G_Condition> preconditions,
            List<G_Condition> effects,
            int cost = 10)
        {
            this.name = name;
            this.preconditions = preconditions;
            this.effects = effects;
            this.cost = cost;
        }
        #endregion
    }
}
