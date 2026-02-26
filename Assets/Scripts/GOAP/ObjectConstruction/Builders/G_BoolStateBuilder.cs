using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace GOAP
{
    public class G_BoolStateBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";
        bool value = false;

        // Constructor
        public G_BoolStateBuilder()
        {

        }
        #endregion

        #region With Functions
        // with functions
        public G_BoolStateBuilder WithName(string name)
        {
            this.name = name;
            return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                // Instead, using return.this allows to chain on to the end of it. 
        }

        public G_BoolStateBuilder WithValue(bool value)
        {
            this.value = value;
            return this;
        }
        #endregion

        #region ObjectCreation

        public G_BoolState Build()          // Instead of all of this, we could have simply set up the Constructor taking the 2 parameters (name and value),
                                            // but using a Builder make the code self documentating, easier to read and is especially useful when we have a lot of parameters to take.
                                            // On top of this, with the Builder you can use only the parameters needed, and put them in any order
                                            // while a constructor will need each parameters that it had been configured to take in a specific order
        {
            // G_BoolState state = new G_BoolState();       // We can't do this with Unity because G_BoolState inherits from a ScriptableObject. We use new only with normal C# class
            // If an object inherits from a ScriptableObject (or MonoBehaviour), we must use ScriptableObject.CreateInstance: 
            G_BoolState state = ScriptableObject.CreateInstance<G_BoolState>();

            state.Construct(name, value);
            return state;
        }

        public static implicit operator G_BoolState (G_BoolStateBuilder builder)        // This tells the compiler: “If someone tries to use a G_BoolStateBuilder where a G_BoolState is expected… automatically call Build().”
                                                                                        // This triggers only when the compiler needs a G_BoolState.
        {
            return builder.Build();
        }

        #endregion
    }
}
