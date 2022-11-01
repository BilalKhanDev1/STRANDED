using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item Item;

    public bool IsEmpty => Item == null;

    public void SetItem(Item item)
    {
        Item = item;
    }
}
