using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;
    [SerializeField] Image _progressBarFilledImage;
    [SerializeField] GameObject _progressBar;

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
        {
            _progressBarFilledImage.fillAmount = InspectionManager.InspectionProgress;
            _progressBar.SetActive(true);
        }
        else if (_progressBar.activeSelf)
        {
            _progressBar.SetActive(false);
        }
    }
}
