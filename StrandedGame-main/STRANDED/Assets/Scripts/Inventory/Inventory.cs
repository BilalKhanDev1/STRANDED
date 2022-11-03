using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int GeneralSize = 9;
    const int CraftingSize = 9;

    public ItemSlot[] GeneralSlot = new ItemSlot[GeneralSize];
    public ItemSlot[] CraftingSlot = new ItemSlot[CraftingSize];
    public List<ItemSlot> OverflowSlot = new List<ItemSlot>();
    
    [SerializeField] Item _debugItem;

    public static Inventory Instance { get; private set; }
    public ItemSlot TopOverflowSlot => OverflowSlot?.FirstOrDefault();

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < GeneralSize; i++)
            GeneralSlot[i] = new ItemSlot();
        for (int i = 0; i < CraftingSize; i++)
            CraftingSlot[i] = new ItemSlot();
    }

    bool AddItemToSlots(Item item, IEnumerable<ItemSlot> slots)
    {
        var stackableSlot = slots.FirstOrDefault(t => t.Item == item && t.HasStackSpaceAvailable);
        if (stackableSlot != null)
        {
            stackableSlot.ModifyStack(1);
            return true;
        }

        var slot = slots.FirstOrDefault(t => t.IsEmpty);
        if (slot != null)
        {
            slot.SetItem(item);
            return true;
        }
        return false;
    }

    public void AddItem(Item item, InventoryType preferredItemType = InventoryType.General)
    {
        var preferredSlots = preferredItemType == InventoryType.General ? GeneralSlot : CraftingSlot;
        var backupSlots = preferredItemType == InventoryType.General ? CraftingSlot : GeneralSlot;

        if (AddItemToSlots(item, preferredSlots))
            return;

        if (AddItemToSlots(item, backupSlots))
            return;

        if (AddItemToSlots(item, OverflowSlot))
            return;
    }

    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem() => AddItem(_debugItem);

    [ContextMenu(nameof(MoveItemsRight))]
    void MoveItemsRight()
    {
        Item lastItem = GeneralSlot.Last().Item;

        for (int i = GeneralSize - 1; i > 0; i--)
        {
            GeneralSlot[i].SetItem(GeneralSlot[i - 1].Item);
        }
        
        GeneralSlot.First().SetItem(lastItem);
    }
    
    public void Bind(List<SlotData> slotDatas)
    {
        var overflowSlot = new ItemSlot();
        var overflowSlotData = new SlotData() { SlotName = "Overflow" + OverflowSlot.Count };
        slotDatas.Add(overflowSlotData);
        overflowSlot.Bind(overflowSlotData);
        OverflowSlot.Add(overflowSlot);

        for (int i = 0; i < GeneralSlot.Length; i++)
        {
            var slot = GeneralSlot[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == "General" + i);
            if (slotData == null)
            {
                slotData = new SlotData() {SlotName = "General" + i };
                slotDatas.Add(slotData);
            }
            slot.Bind(slotData);
        }

        for (int i = 0; i < CraftingSlot.Length; i++)
        {
            var slot = CraftingSlot[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == "Crafting" + i);
            if (slotData == null)
            {
                slotData = new SlotData() { SlotName = "Crafting" + i };
                slotDatas.Add(slotData);
            }
            slot.Bind(slotData);
        }
    }
    
    public void ClearCraftingSlots()
    {
        foreach (var slot in CraftingSlot)
            slot.RemoveItem();
    }
}

public enum InventoryType
{
    General,
    Crafting
}