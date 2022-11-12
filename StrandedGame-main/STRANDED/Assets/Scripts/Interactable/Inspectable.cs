using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{

    private static HashSet<Inspectable> _inspectablesInRange = new HashSet<Inspectable>();
    public static IReadOnlyCollection<Inspectable> InspectablesInRange => _inspectablesInRange;

    public static event Action<bool> InspectablesInRangeChanged;
    public static event Action<Inspectable, string> AnyInspectionComplete;

    [SerializeField] float _timeToInspect = 3f;
    [SerializeField, TextArea] string _completedInspectionText;
    [SerializeField] UnityEvent OnInspectionCompleted;

    InspectableData _data;
    public bool WasFullyInspected => InspectionProgress >= 1;
    public float InspectionProgress => (_data?.TimeInspected ?? 0f) / _timeToInspect;

    public void Bind(InspectableData inspectableData)
    {
        _data = inspectableData;
        if (WasFullyInspected) { RestoreInspectionState(); }
    }

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
            if (_inspectablesInRange.Remove(this))
                InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        }
    }

    public void Inspect()
    {
        if (WasFullyInspected)
            return;

        _data.TimeInspected += Time.deltaTime;
        if (WasFullyInspected) 
            CompleteInspection();
    }

    void CompleteInspection()
    {
        _inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();
        AnyInspectionComplete?.Invoke(this, _completedInspectionText);
    }

    public void RestoreInspectionState() => OnInspectionCompleted?.Invoke();
}