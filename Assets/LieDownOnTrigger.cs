using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieDownOnTrigger : MonoBehaviour
{
    private Animator playerAnimator;
    private Movement playerMovementScript;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponentInChildren<Animator>();
        playerMovementScript = player.GetComponent<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerAnimator.SetTrigger("Overwhelm");
        playerMovementScript.DeadEnd();
    }
}
