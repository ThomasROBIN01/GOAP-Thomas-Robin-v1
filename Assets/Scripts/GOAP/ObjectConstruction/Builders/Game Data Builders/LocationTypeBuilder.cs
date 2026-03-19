using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class LocationTypeBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";

        // Constructor
        public LocationTypeBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region With Functions
        
        //public LocationTypeBuilder WithName(string name)
        //{
        //    this.name = name;
        //    return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
        //                        // Instead, using return.this allows to chain on to the end of it. 
        //}

        #endregion

        #region Object Creation

        /// <summary>
        /// replace LocationType type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public LocationType Build()         
        {
            LocationType location = ScriptableObject.CreateInstance<LocationType>();
            location.name = name;
            return location;
        }

        public static implicit operator LocationType(LocationTypeBuilder builder)        // This tells the compiler: “If someone tries to use a LocationTypeBuilder where an "LocationType" type is expected… automatically call Build().”
                                                                                         // This triggers only when the compiler needs an "LocationType" type.
        {
            return builder.Build();
        }

        #endregion

    }
}
