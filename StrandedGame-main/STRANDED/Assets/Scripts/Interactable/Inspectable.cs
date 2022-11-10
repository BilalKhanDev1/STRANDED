using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    [SerializeField] float _timetoInspect = 3f;
    [SerializeField] UnityEvent OnInspectionCompleted;
    float _timeInspected;

    static HashSet<Inspectable> _inspectablesInRange = new HashSet<Inspectable>();

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => _inspectablesInRange;
    public static event Action<bool> InspectablesInRangeChanged;

    public bool WasFullyInspected { get; private set; }

    public float InspectionProgress => _timeInspected / _timetoInspect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && WasFullyInspected == false)
        {
            _inspectablesInRange.Add(this);
            InspectablesInRangeChanged?.Invoke(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             if (_inspectablesInRange.Remove(this));
            InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        }
    }

    public void Inspect()
    {
        _timeInspected += Time.deltaTime;
        if (_timeInspected >= _timetoInspect)
            CompleteInspection();
    }

    void CompleteInspection()
    {
        WasFullyInspected = true;
        _inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();
    }
}
