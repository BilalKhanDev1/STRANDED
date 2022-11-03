using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public void TryCrafting()
    {
        int itemsInCraftingInventory = Inventory.Instance.CraftingSlot.Where(t => t.IsEmpty == false).Count();
    }
}
