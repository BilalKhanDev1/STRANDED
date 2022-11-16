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
    List<SlotData> _slotDatas;
    
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

    public void AddItemFromEvent(Item item) => AddItem(item);

    public void AddItem(Item item, InventoryType preferredItemType = InventoryType.General)
    {
        var preferredSlots = preferredItemType == InventoryType.General ? GeneralSlot : CraftingSlot;
        var backupSlots = preferredItemType == InventoryType.General ? CraftingSlot : GeneralSlot;

        if (AddItemToSlots(item, preferredSlots))
            return;

        if (AddItemToSlots(item, backupSlots))
            return;
    }

    public void Bind(List<SlotData> slotDatas)
    {
        _slotDatas = slotDatas;

        for (int i = 0; i < GeneralSlot.Length; i++)
        {
            var slot = GeneralSlot[i];
            var slotData = slotDatas.FirstOrDefault(t => t.SlotName == "General" + i);
            if (slotData == null)
            {
                slotData = new SlotData() { SlotName = "General" + i };
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

    public void RemoveItemFromSlot(ItemSlot itemSlot)
    {
        itemSlot.RemoveItem();
    }

    public void Swap(ItemSlot sourceSlot, ItemSlot targetSlot)
    {
        if (targetSlot == TopOverflowSlot)
        {
            return;
        }
        else if (sourceSlot == TopOverflowSlot)
        {
            MoveItemFromOverflowSlot(targetSlot);
        }
        else if (targetSlot != null && targetSlot.IsEmpty && Input.GetKey(KeyCode.LeftShift))
        {

            targetSlot.SetItem(sourceSlot.Item) ;
            sourceSlot.ModifyStack(-1);
        }
        else if (targetSlot != null && targetSlot.Item == sourceSlot.Item && targetSlot.HasStackSpaceAvailable)
        {
            int numberToMove = Mathf.Min(targetSlot.AvailableStackSpace, sourceSlot.StackCount);
            if (Input.GetKey(KeyCode.LeftShift) && numberToMove > 1)
                numberToMove = 1;
            targetSlot.ModifyStack(numberToMove);
            sourceSlot.ModifyStack(-numberToMove);
        }
        else
        sourceSlot.Swap(targetSlot);
    }

    void MoveItemFromOverflowSlot(ItemSlot focusedItemSlot)
    {
        focusedItemSlot.SetItem(TopOverflowSlot.Item);
    }
}

public enum InventoryType
{
    General,
    Crafting
}