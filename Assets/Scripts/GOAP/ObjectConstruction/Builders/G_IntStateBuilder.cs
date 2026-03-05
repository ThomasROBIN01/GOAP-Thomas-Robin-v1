using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class G_IntStateBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";
        int value;

        // Constructor
        public G_IntStateBuilder()
        {

        }
        #endregion

        #region With Functions
        // with functions
        public G_IntStateBuilder WithName(string name)
        {
            this.name = name;
            return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                // Instead, using return.this allows to chain on to the end of it. 
        }

        public G_IntStateBuilder WithValue(int value)
        {
            this.value = value;
            return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                // Instead, using return.this allows to chain on to the end of it. 
        }

        #endregion

        #region ObjectCreation

        /// <summary>
        /// replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_IntState Build()         
        {
            G_IntState state = ScriptableObject.CreateInstance<G_IntState>();
            state.Construct(name, value);

            return state;
        }

        public static implicit operator G_IntState(G_IntStateBuilder builder)        // This tells the compiler: “If someone tries to use a G_IntStateBuilder where an "G_IntState" type is expected… automatically call Build().”
                                                                                     // This triggers only when the compiler needs an "G_IntState" type.
        {
            return builder.Build();
        }


        #endregion

    }
}
