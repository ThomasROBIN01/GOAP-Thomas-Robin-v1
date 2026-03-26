using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class ItemBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";
        bool stackable = true;

        // Constructor
        public ItemBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region With Functions
        // with functions
        public ItemBuilder IsStackable(bool stackable)
        {
            this.stackable = stackable;
            return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                // Instead, using return.this allows to chain on to the end of it. 
        }
        //public ItemBuilder WithName(string name)
        //{
        //    this.name = name;
        //    return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
        //                        // Instead, using return.this allows to chain on to the end of it. 
        //}

        #endregion

        #region ObjectCreation

        /// <summary>
        /// replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public Item Build()         
        {
            Item item = ScriptableObject.CreateInstance<Item>();
            item.stackable = stackable;
            return item;
        }

        public static implicit operator Item(ItemBuilder builder)        // This tells the compiler: “If someone tries to use a ItemBuilder where an "Item" type is expected… automatically call Build().”
                                                                         // This triggers only when the compiler needs an "Item" type.
        {
            return builder.Build();
        }
        // We got an error with this function because we can't convert an Item to a ItemBuilder, so we just comment this for now
        // and will uncomment and change this depending on the class builder

        #endregion

    }
}
