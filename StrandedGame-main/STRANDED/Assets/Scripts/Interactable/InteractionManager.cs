using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    static Interactable _currentInteractable;
    public static bool Interacting { get; private set; }
    public static float InteractionProgress => _currentInteractable?.InteractionProgress ?? 0f;


    void Awake() => Interactable.InteractablesInRangeChanged += HandleInteractablesInRangeChanged;

    void OnDestroy() => Interactable.InteractablesInRangeChanged -= HandleInteractablesInRangeChanged;

    void HandleInteractablesInRangeChanged(bool obj)
    {
        var nearest = Interactable.interactablesInRange.OrderBy(t => Vector3.Distance(t.transform.position, transform.position)).FirstOrDefault();

        _currentInteractable = nearest;
    }

    void Update()
    {
        if (_currentInteractable != null && Input.GetKey(_currentInteractable.Hotkey))
        {
            _currentInteractable.Interact();
            Interacting = true;
        }
        else 
            Interacting = false;
    }

    public static void Bind(List<InteractableData> datas)
    {
        var allInteractales = GameObject.FindObjectsOfType<Interactable>(true);
        foreach (var interactable in allInteractales)
        {
            var data = datas.FirstOrDefault(t => t.Name == interactable.name);
            if (data == null)
            {
                data = new InteractableData() { Name = interactable.name };
                datas.Add(data);
            }
            interactable.Bind(data);
        }
    }
}