using UnityEngine;
using TMPro;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;
    [SerializeField] TMP_Text _progressText;

    void OnEnable()
    {
        _hintText.enabled = false;
        Inspectable.InspectablesInRangeChanged += UpdateHintTextState;
    }

    void OnDisable() => Inspectable.InspectablesInRangeChanged -= UpdateHintTextState;

    void UpdateHintTextState(bool enableHint) => _hintText.enabled = enableHint;

    void Update()
    {
        if (InspectionManager.Inspecting)
            _progressText.SetText(InspectionManager.InspectionProgress.ToString());
    }
}
