using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class GatherWoodTestData
    {
        public const int woodStockMax = 9999;   // const: to keep it as it is and not be able to modify it

        #region Data and Objects

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

        #region GameObjects
        public GameObject npc_object;
        public GameObject tree_object;
        public GameObject workshop_object;
        public GameObject woodstock_object;
        public GameObject shop_object;
        #endregion

        #region Inventories
        public Inventory npc_inventory_component;
        public Inventory workshop_inventory_component;
        public Inventory woodstock_inventory_component;
        public Inventory tree_inventory_component;
        public Inventory shop_inventory_component;
        #endregion

        #endregion

        #region GOAP

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

        #region World States
        public G_WorldState npc_world_state;
        #endregion

        #endregion

        public GatherWoodTestData()
        {
            #region Data Creation

            chopped_wood = An.Item("chopped_wood").IsStackable(true);
            axe = An.Item("chopped_wood").IsStackable(false);
            money = An.Item("chopped_wood").IsStackable(true);

            tree = A.LocationType("tree");
            workshop = A.LocationType("workshop");
            woodstock = A.LocationType("woodstock");
            shop = A.LocationType("shop");

            #endregion

            #region Objects and Component Creation

            npc_object = new GameObject();
            tree_object = new GameObject();
            workshop_object = new GameObject();
            woodstock_object = new GameObject();
            shop_object = new GameObject();

            npc_inventory_component = npc_object.AddComponent<Inventory>();
            tree_inventory_component = tree_object.AddComponent<Inventory>();
            workshop_inventory_component = workshop_object.AddComponent<Inventory>();
            woodstock_inventory_component = woodstock_object.AddComponent<Inventory>();
            shop_inventory_component = shop_object.AddComponent<Inventory>();

            #endregion


            #region State Creation

            npc_inventory = An.InventoryState("npc_inventory").WithInventory(npc_inventory_component);
            workshop_inventory = An.InventoryState("workshop_inventory").WithInventory(workshop_inventory_component);
            woodstock_inventory = An.InventoryState("woodstock_inventory").WithInventory(woodstock_inventory_component);
            tree_inventory = An.InventoryState("tree_inventory").WithInventory(tree_inventory_component);
            shop_inventory = An.InventoryState("shop_inventory").WithInventory(shop_inventory_component);
            atLocation = An.AtLocation().WithName("at_location");

            #endregion

        }

    }
}
