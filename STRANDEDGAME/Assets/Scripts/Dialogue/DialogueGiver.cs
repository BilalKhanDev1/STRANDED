using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGiver : MonoBehaviour
{
    [SerializeField] TextAsset _dialog;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            FindObjectOfType<DialogueController>().StartDialog(_dialog);
            transform.LookAt(player.transform);
        }
    }
}

