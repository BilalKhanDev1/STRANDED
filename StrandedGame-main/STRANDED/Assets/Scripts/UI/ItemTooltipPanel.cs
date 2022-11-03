using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTooltipPanel : MonoBehaviour
{
    public static ItemTooltipPanel Instance { get; private set; }

    [SerializeField] TMP_Text _name;
    [SerializeField] TMP_Text _description;
    [SerializeField] Image _icon;

    CanvasGroup _canvasGroup;

    void Awake()
    {
        Instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowItem(Item item)
    {
        if (item == null)
        {
            Toggle(false);
        }
        else
        {
            Toggle(true);
            _name.SetText(item.name);
            _description.SetText(item.Description);
            _icon.sprite = item.Icon;
        }
    }

    void Toggle(bool visible)
    {
        _canvasGroup.alpha = visible ? 1f : 0;
        _canvasGroup.interactable = visible;
        _canvasGroup.blocksRaycasts = visible;
    }

}
