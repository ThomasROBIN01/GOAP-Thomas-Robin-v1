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
        public G_AtLocation at_location;
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
            at_location = An.AtLocation().WithName("at_location");

            #endregion

            #region Action Generation

            deliver_wood = An.Action("deliver_wood").WithPrecondition(A.Condition().WithState(npc_inventory)
                                                                                   .WithComparison(G_StateComparison.greaterOrEqual)
                                                                                   .WithExpectedValue(new ItemStack(chopped_wood, 10)))     // to deliver wood, we need to have more than 10 chopped wood

                                                    .WithPrecondition(A.Condition().WithState(at_location)
                                                                                   .WithExpectedValue(woodstock))       // we need to be at the woodstock location



                                                    .WithEffect(A.Condition().WithState(woodstock_inventory)
                                                                             .WithComparison(G_StateComparison.equal)
                                                                             .WithExpectedValue(new ItemStack(chopped_wood, woodStockMax)))     // we'll just have the woodstock inventory filled up as an effect for this test

                                                    .WithEffect(A.Condition().WithState(npc_inventory)
                                                                             .WithComparison(G_StateComparison.equal)
                                                                             .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))    // the npc inventory will be emptied of the chopped wood

                                                    .WithCost(10);


            go_to_woodstock = An.Action("go_to_woodstock").WithEffect(A.Condition().WithState(at_location).WithExpectedValue(woodstock))
                                                          .WithCost(10);


            chop_tree = An.Action("chop_tree").WithPrecondition(A.Condition().WithState(npc_inventory)
                                                                             .WithComparison(G_StateComparison.greater)
                                                                             .WithExpectedValue(ItemStack.EmptyStack(axe)))       // put an axe in the npc inventory

                                              .WithPrecondition(A.Condition().WithState(at_location)
                                                                             .WithExpectedValue(tree))        // we need to be at the tree location to cut it



                                              .WithEffect(A.Condition().WithState(npc_inventory)
                                                                       .WithComparison(G_StateComparison.greaterOrEqual)
                                                                       .WithExpectedValue(new ItemStack(chopped_wood, 10)))   // chopping a tree will give 10 chopped wood

                                              .WithEffect(A.Condition().WithState(tree_inventory)
                                                                       .WithComparison(G_StateComparison.equal)
                                                                       .WithExpectedValue(ItemStack.EmptyStack(chopped_wood))) // the tree inventory will be fully emptied for this test

                                              .WithCost(10);


            go_to_tree = An.Action("go_to_tree").WithEffect(A.Condition().WithState(at_location).WithExpectedValue(tree))
                                                .WithCost(10);


            take_axe = An.Action("take_axe").WithPrecondition(A.Condition().WithState(npc_inventory)
                                                                           .WithComparison(G_StateComparison.equal)
                                                                           .WithExpectedValue(ItemStack.EmptyStack(axe)))   // we need the npc inventory to have no axe to be able to take one

                                            .WithPrecondition(A.Condition().WithState(workshop_inventory)
                                                                           .WithComparison(G_StateComparison.greater)
                                                                           .WithExpectedValue(ItemStack.EmptyStack(axe)))   // workshop inventory needs to have one axe available

                                            .WithPrecondition(A.Condition().WithState(at_location).WithExpectedValue(workshop))     // we need to be at the workshop to take the axe



                                            .WithEffect(A.Condition().WithState(npc_inventory)
                                                                     .WithComparison(G_StateComparison.greater)
                                                                     .WithExpectedValue(ItemStack.EmptyStack(axe)))   // axe will be greater than 0 in the npc_inventory

                                            .WithEffect(A.Condition().WithState(workshop_inventory)
                                                                     .WithComparison(G_StateComparison.equal)
                                                                     .WithExpectedValue(ItemStack.EmptyStack(axe)))   // the workshop will now have no axe in for this test

                                            .WithCost(10);


            go_to_workshop = An.Action("go_to_workshop").WithEffect(A.Condition().WithState(at_location).WithExpectedValue(workshop))
                                    .WithCost(10);


            buy_wood = An.Action("buy_wood").WithPrecondition(A.Condition().WithState(npc_inventory)
                                                                           .WithComparison(G_StateComparison.equal)
                                                                           .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))   // we need the npc inventory to have no chopped wood to be able to buy some

                                            .WithPrecondition(A.Condition().WithState(npc_inventory)
                                                                           .WithComparison(G_StateComparison.greater)
                                                                           .WithExpectedValue(ItemStack.EmptyStack(money)))   // we also need the npc inventory to have some money (>0)

                                            .WithPrecondition(A.Condition().WithState(shop_inventory)
                                                                           .WithComparison(G_StateComparison.greater)
                                                                           .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))   // workshop inventory needs to have some chopped wood (>0)

                                            .WithPrecondition(A.Condition().WithState(at_location).WithExpectedValue(shop))     // we need to be at the shop



                                            .WithEffect(A.Condition().WithState(npc_inventory)
                                                                     .WithComparison(G_StateComparison.greaterOrEqual)
                                                                     .WithExpectedValue(new ItemStack(chopped_wood, 10)))   // the npc inventory will then have at least 10 chopped wood

                                            .WithEffect(A.Condition().WithState(shop_inventory)
                                                                     .WithComparison(G_StateComparison.equal)
                                                                     .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))   // the shop will be emptied of chopped wood for this test

                                            .WithEffect(A.Condition().WithState(npc_inventory)
                                                                     .WithComparison(G_StateComparison.equal)
                                                                     .WithExpectedValue(ItemStack.EmptyStack(money)))   // the npc inventory will have no money after buying the wood for this test

                                            .WithCost(10);


            go_to_shop = An.Action("go_to_shop").WithEffect(A.Condition().WithState(at_location).WithExpectedValue(shop))
                        .WithCost(10);

            #endregion
        }

    }
}
