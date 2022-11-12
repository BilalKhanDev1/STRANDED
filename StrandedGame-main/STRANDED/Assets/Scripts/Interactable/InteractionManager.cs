using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    static Interactable _currentInteractable;
    public static bool Interacting => _currentInteractable != null && _currentInteractable.WasFullyInteracted == false;
    public static float InteractionProgress => _currentInteractable?.InteractionProgress ?? 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _currentInteractable = Interactable.interactablesInRange.FirstOrDefault();

        if (Input.GetKey(KeyCode.E) && _currentInteractable != null)
            _currentInteractable.Interact();
        else
            _currentInteractable = null;
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