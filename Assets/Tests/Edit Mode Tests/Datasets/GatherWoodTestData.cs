using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class GatherWoodTestData
    {
        public const int woodStockMax = 9999;   // const: to keep it as it is and not be able to modify it

        #region World States
        public G_WorldState npc_world_state;
        #endregion

        #region Items
        public Item chopped_wood;
        public Item axe;
        public Item money;
        #endregion

        #region Location Types
        public LocationType tree;
        public LocationType workshop;
        public LocationType woodstock;
        public LocationType shop;
        #endregion

        #region Inventories
        public Inventory npc_inventory_component;
        public Inventory workshop_inventory_component;
        public Inventory woodstock_inventory_component;
        public Inventory tree_inventory_component;
        public Inventory shop_inventory_component;
        #endregion

        #region GameObjects
        public GameObject npc_object;
        public GameObject tree_object;
        public GameObject workshop_object;
        public GameObject woodstock_object;
        public GameObject shop_object;
        #endregion

        #region States
        public G_Inventory npc_inventory;
        public G_Inventory workshop_inventory;
        public G_Inventory woodstock_inventory;
        public G_Inventory tree_inventory;
        public G_Inventory shop_inventory;
        public G_AtLocation atLocation;
        #endregion

        #region Actions
        public G_Action deliver_wood;
        public G_Action go_to_woodstock;
        public G_Action chop_tree;
        public G_Action go_to_tree;
        public G_Action take_axe;
        public G_Action go_to_workshop;
        public G_Action buy_wood;
        public G_Action go_to_shop;
        #endregion

        #region Goals
        public G_Goal gather_wood;
        #endregion


    }
}
