using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inspectable : MonoBehaviour
{
    [SerializeField] float _timetoInspect = 3f;
    float _timeInspected;

    static HashSet<Inspectable> _inspectablesInRange = new HashSet<Inspectable>();

    public static IReadOnlyCollection<Inspectable> InspectablesInRange => _inspectablesInRange;

    public float InspectionProgress => _timeInspected / _timetoInspect;

    public static event Action<bool> InspectablesInRangeChanged;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inspectablesInRange.Add(this);
            InspectablesInRangeChanged?.Invoke(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inspectablesInRange.Remove(this);
            InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        }
    }

    public void Inspect()
    {
        _timeInspected += Time.deltaTime;
        if (_timeInspected >= _timetoInspect)
        {
            CompleteInspection();
        }
    }

    void CompleteInspection()
    {
        _inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        gameObject.SetActive(false);
    }
}
