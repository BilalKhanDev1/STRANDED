using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject Club;
    bool canSwing = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSwing == true)
            StartCoroutine(SwordSwing());
    }

    IEnumerator SwordSwing()
    {
        Club.GetComponent<Animator>().Play("SwingAnim");
        canSwing = false;
        yield return new WaitForSeconds(0.8f);
        Club.GetComponent<Animator>().Play("New State");
        canSwing = true;
    }
}
