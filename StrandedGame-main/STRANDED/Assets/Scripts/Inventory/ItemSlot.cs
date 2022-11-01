using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item Item;
    SlotData _slotData;

    public bool IsEmpty => Item == null;

    public void SetItem(Item item)
    {
        Item = item;
        _slotData.ItemName = item?.name ?? string.Empty;
    }

    public void Bind(SlotData slotData)
    {
        _slotData = slotData;
        var item = Resources.Load<Item>("Items/" + _slotData.ItemName);
        SetItem(item);
    }
}

[Serializable]

public class SlotData
{
    public string SlotName;
    public string ItemName;
}
