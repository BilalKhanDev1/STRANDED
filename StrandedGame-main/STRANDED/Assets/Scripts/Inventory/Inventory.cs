using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int GeneralSize = 9;

    public ItemSlot[] GeneralInventory = new ItemSlot[GeneralSize];

    [SerializeField] Item _debugItem;

    private void Awake()
    {
        for (int i = 0; i < GeneralSize; i++)
            GeneralInventory[i] = new ItemSlot();
    }

    public void AddItem(Item item)
    {
        var firstAvailableSlot = GeneralInventory.FirstOrDefault(t => t.IsEmpty);
        firstAvailableSlot.SetItem(item);
    }

    [ContextMenu(nameof(AddDebugItem))]
    void AddDebugItem() => AddItem(_debugItem);

    [ContextMenu(nameof(MoveItemsRight))]
    void MoveItemsRight()
    {
        var lastItem = GeneralInventory.Last().Item;
        for (int i = GeneralSize - 1; i >= 0; i--)
        {
            GeneralInventory[i].SetItem(GeneralInventory[i - 1].Item);
        }
        GeneralInventory.First().SetItem(lastItem);
    }
}