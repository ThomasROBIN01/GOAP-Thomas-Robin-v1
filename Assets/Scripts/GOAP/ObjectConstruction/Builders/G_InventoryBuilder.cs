using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// This is just a template that will be used as a base and renamed with the actual class builder name that we want to create, rather than having to rewrite all of this everytime.
    /// </summary>
    public class G_InventoryBuilder
    {
        #region Basic Values

        // any values to be transferred into the build object
        string name = "";
        Inventory inventory;

        // Constructor
        public G_InventoryBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region With Functions

        public G_InventoryBuilder WithInventory(Inventory inventory)
        {
            this.inventory = inventory;
            return this;        // return this instance. Not returning, will require to call this function accross multiple lines (see commented example in the BoolStateTests script)
                                // Instead, using return.this allows to chain on to the end of it. 
        }

        #endregion

        #region ObjectCreation

        /// <summary>
        /// replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_Inventory Build()         
        {
            G_Inventory inventoryState = ScriptableObject.CreateInstance<G_Inventory>();
            inventoryState.Construct(name, inventory);
            return inventoryState;
        }

        public static implicit operator G_Inventory(G_InventoryBuilder builder)        // This tells the compiler: “If someone tries to use a G_InventoryBuilder where an "G_Inventory" type is expected… automatically call Build().”
                                                                                       // This triggers only when the compiler needs an "G_Inventory" type.
        {
            return builder.Build();
        }
        // We got an error with this function because we can't convert an object to a G_InventoryBuilder, so we just comment this for now
        // and will uncomment and change this depending on the class builder

        #endregion

    }
}
