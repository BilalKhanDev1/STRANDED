using System.Collections;
using UnityEngine;

public class GuidePanels : MonoBehaviour
{
    public GameObject panel;

    public void ShowTip()
    {
        panel.SetActive(true);
        StartCoroutine(ShowPanel());
    }

    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(8f);
        panel.SetActive(false);
    }
}
