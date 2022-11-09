using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] float _rotateRate = 1000f;

    public ItemSlot _itemSlot;
    private GameObject _placeable;
    public static PlacementManager Instance { get; private set; }

    private void Awake() => Instance = this;

    public void BeginPlacement(ItemSlot itemSlot)
    {
        if (itemSlot == null || itemSlot.Item == null || itemSlot.Item.PlaceablePrefab == null)
        {
            Debug.Log($"unable to place slot item (something's null)");
        }
        _itemSlot = itemSlot;
        Debug.Log($"Started placing item {_itemSlot.Item}");

        _placeable = Instantiate(_itemSlot.Item.PlaceablePrefab);
        _placeable.transform.SetParent(transform);
    }

    private void Update()
    {
        if (_placeable == null) { return; }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _placeable.transform.position = hitInfo.point;
        }

        var rotation = -Input.mouseScrollDelta.y * Time.deltaTime * _rotateRate;
        _placeable.transform.Rotate(0, rotation, 0);
    }
}
