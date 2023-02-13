using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckPointOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement playerMovementScript = collision.gameObject.GetComponent<Movement>();

        playerMovementScript.SetCheckPoint(transform);
    }
}
