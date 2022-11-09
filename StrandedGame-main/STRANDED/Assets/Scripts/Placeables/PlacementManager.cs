using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] ItemSlot _itemSlot;

    public static PlacementManager Instance { get; private set; }

    void Awake() => Instance = this;

    public void BeginPlacement(ItemSlot itemSlot)
    {
        if (itemSlot == null || itemSlot.Item == null || itemSlot.Item.PlaceablePrefab == null)
        {
            return;
        }
        
        _itemSlot = itemSlot;

        var placeable = Instantiate(itemSlot.Item.PlaceablePrefab);
        placeable.transform.SetParent(transform);
    }
}
