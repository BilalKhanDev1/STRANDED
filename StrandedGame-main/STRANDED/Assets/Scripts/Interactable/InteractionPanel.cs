using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class InteractionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;
    [SerializeField] TMP_Text _completedInteractionText;
    [SerializeField] Image _progressBarFilledImage;
    [SerializeField] GameObject _progressBar;

    void OnEnable()
    {
        _hintText.enabled = false;
        _completedInteractionText.enabled = false;

        Interactable.InteractablesInRangeChanged += UpdateHintTextState;
        Interactable.AnyInteractionComplete += ShowCompletedInspectionText;
    }

    void ShowCompletedInspectionText(Interactable inspectable, string message)
    {
        _completedInteractionText.SetText(message);
        _completedInteractionText.enabled = true;
        float msgTime = message.Length / 5f;
        msgTime = Mathf.Clamp(msgTime, 3f, 15f);
        StartCoroutine(FadeCompletedText(msgTime));
    }

    IEnumerator FadeCompletedText(float msgTime)
    {
        _completedInteractionText.alpha = 1f;
        while (_completedInteractionText.alpha > 0)
        {
            yield return null;
            _completedInteractionText.alpha -= Time.deltaTime / msgTime;
        }
        _completedInteractionText.enabled = false;
    }

    void OnDisable() => Interactable.InteractablesInRangeChanged -= UpdateHintTextState;

    void UpdateHintTextState(bool enableHint) => _hintText.enabled = enableHint;

    void Update()
    {
        if (InteractionManager.Interacting)
        {
            _progressBarFilledImage.fillAmount = InteractionManager.InteractionProgress;
            _progressBar.SetActive(true);
        }
        else if (_progressBar.activeSelf)
            _progressBar.SetActive(false);
    }
}