using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPanelSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    static InventoryPanelSlot Focused;

    ItemSlot _itemSlot;
    [SerializeField] Image _draggedItemIcon;
    [SerializeField] Image _itemIcon;
    [SerializeField] Outline _outline;
    [SerializeField] Color _draggingColor = Color.grey;

    public void Bind(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemSlot.Changed += UpdateIcon;

        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (_itemSlot.Item != null)
        {
            _itemIcon.sprite = _itemSlot.Item.Icon;
            _itemIcon.enabled = true;
        }
        else
        {
            _itemIcon.sprite = null;
            _itemIcon.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Focused = this;
        _outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Focused == this)
            Focused = null;

        _outline.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_itemSlot.IsEmpty)
            return;

        _itemIcon.color = _draggingColor;
        _draggedItemIcon.sprite = _itemIcon.sprite;
        _draggedItemIcon.enabled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_itemSlot.IsEmpty == false && Focused != null)
            _itemSlot.Swap(Focused._itemSlot);
        
        _itemIcon.color = Color.white;
        _draggedItemIcon.sprite = null;
        _draggedItemIcon.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _draggedItemIcon.transform.position = eventData.position;
    }
}
