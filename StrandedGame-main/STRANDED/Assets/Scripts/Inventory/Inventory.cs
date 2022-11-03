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

    [SerializeField] Item _debugItem;

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < GeneralSize; i++)
            GeneralSlot[i] = new ItemSlot();
        for (int i = 0; i < CraftingSize; i++)
            CraftingSlot[i] = new ItemSlot();
    }

    public void AddItem(Item item)
    {
        var firstAvailableSlot = GeneralSlot.FirstOrDefault(t => t.IsEmpty);
        firstAvailableSlot.SetItem(item);
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
}