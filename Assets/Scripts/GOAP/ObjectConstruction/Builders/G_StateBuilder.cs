using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class G_StateBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";
        bool isLocal = false;
        object value = null;

        // Constructor
        public G_StateBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region With Functions
        // with functions
        public G_StateBuilder WithValue(object value)
        {
            this.value = value;
            return this;         // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                 // Instead, using return.this allows to chain on to the end of it. 
        }

        #endregion

        #region ObjectCreation

        /// <summary>
        /// replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_State Build()         
        {
            G_State state = ScriptableObject.CreateInstance<G_State>();
            state.Construct(this.name, this.value, this.isLocal);
            return state;
        }

        public static implicit operator G_State(G_StateBuilder builder)        // This tells the compiler: “If someone tries to use a BuilderTemplate where an "object" type is expected… automatically call Build().”
                                                                               // This triggers only when the compiler needs an "object" type.
        {
            return builder.Build();
        }
        // We got an error with this function because we can't convert an object to a BuilderTemplate, so we just comment this for now
        // and will uncomment and change this depending on the class builder

        #endregion

    }
}
