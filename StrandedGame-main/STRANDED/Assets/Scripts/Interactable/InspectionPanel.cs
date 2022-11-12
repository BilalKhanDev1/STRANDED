using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;
    [SerializeField] TMP_Text _completedInspectionText;
    [SerializeField] Image _progressBarFilledImage;
    [SerializeField] GameObject _progressBar;

    void OnEnable()
    {
        _hintText.enabled = false;
        _completedInspectionText.enabled = false;

        Inspectable.InspectablesInRangeChanged += UpdateHintTextState;
        Inspectable.AnyInspectionComplete += ShowCompletedInspectionText;
    }

    void ShowCompletedInspectionText(Inspectable inspectable, string message)
    {
        _completedInspectionText.SetText(message);
        _completedInspectionText.enabled = true;
        float msgTime = message.Length / 5f;
        msgTime = Mathf.Clamp(msgTime, 3f, 15f);
        StartCoroutine(FadeCompletedText(msgTime));
    }

    IEnumerator FadeCompletedText(float msgTime)
    {
        _completedInspectionText.alpha = 1f;
        while (_completedInspectionText.alpha > 0)
        {
            yield return null;
            _completedInspectionText.alpha -= Time.deltaTime / msgTime;
        }
        _completedInspectionText.enabled = false;
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
            _progressBar.SetActive(false);
    }
}